using FluentValidation;
using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Streaming.Application;
using SpotifyClone.Streaming.Application.Abstractions;
using SpotifyClone.Streaming.Application.Abstractions.Repositories;
using SpotifyClone.Streaming.Application.Abstractions.Services;
using SpotifyClone.Streaming.Application.Behaviors;
using SpotifyClone.Streaming.Application.Errors;
using SpotifyClone.Streaming.Application.Jobs;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets;
using SpotifyClone.Streaming.Domain.Aggregates.ImageAssets;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions;
using SpotifyClone.Streaming.Infrastructure.Media;
using SpotifyClone.Streaming.Infrastructure.Persistence;
using SpotifyClone.Streaming.Infrastructure.Persistence.Database;
using SpotifyClone.Streaming.Infrastructure.Persistence.Repositories;
using SpotifyClone.Streaming.Infrastructure.Storage;

namespace SpotifyClone.Streaming.Infrastructure.DependencyInjection;

public static class StreamingModule
{
    public static IServiceCollection AddStreamingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            StreamingApplicationAssemblyReference.Assembly));

        services.AddValidatorsFromAssembly(StreamingApplicationAssemblyReference.Assembly);

        services.AddDbContext<StreamingAppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("MainDb"),
                b => b.MigrationsAssembly(typeof(StreamingAppDbContext).Assembly.FullName)));

        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMemoryStorage());
        services.AddHangfireServer();

        services.AddHostedService<MinioInitializer>();

        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.SectionName));

        services.AddSingleton<IMediaService, FfmpegMediaService>();
        services.AddSingleton<IFileStorage, MinioFileStorage>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IStreamingUnitOfWork>());
        services.AddScoped<IStreamingUnitOfWork, StreamingEfCoreUnitOfWork>();
        services.AddScoped<IAudioAssetRepository, AudioAssetEfCoreRepository>();
        services.AddScoped<IImageAssetRepository, ImageAssetEfCoreRepository>();
        services.AddScoped<IPlaybackSessionRepository, PlaybackSessionRedisRepository>();
        services.AddScoped<IOutboxRepository, OutboxEfCoreRepository>();
        services.AddScoped<IDomainExceptionMapper, StreamingDomainExceptionMapper>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(StreamingTransactionalPipelineBehavior<,>));
        services.AddTransient<AudioConversionJob>();
        services.AddTransient<ImageConversionJob>();
        services.AddTransient<MarkAudioAssetAsOrphanedJob>();
        services.AddTransient<AudioAssetCleanupJob>();
        services.AddTransient<ImageAssetCleanupJob>();

        return services;
    }

    public static void UseStreamingModule(this IApplicationBuilder app)
    {
        IRecurringJobManager recurringJobManager =
            app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<AudioAssetCleanupJob>(
            "streaming-audio-asset-cleanup",
            job => job.ProcessAsync(),
            Cron.HourInterval(6)
        );

        recurringJobManager.AddOrUpdate<ImageAssetCleanupJob>(
            "streaming-image-asset-cleanup",
            job => job.ProcessAsync(),
            Cron.HourInterval(6)
        );
    }
}
