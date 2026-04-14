using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SpotifyClone.Shared.BuildingBlocks.Application;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Configuration;
using SpotifyClone.Shared.BuildingBlocks.Application.Email;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Auth;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Messaging;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Services;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.DependencyInjection;

public static class BuildingBlocksModule
{
    public static IServiceCollection AddBuildingBlocks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                BuildingBlocksApplicationAssemblyReference.Assembly);

            cfg.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SectionName));
        services.Configure<ApplicationSettings>(configuration.GetSection(ApplicationSettings.SectionName));

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);

        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IEmailSender, SmtpEmailSender>();

        services.AddTransient<IBackgroundJobService, HangfireJobService>();

        return services;
    }
}
