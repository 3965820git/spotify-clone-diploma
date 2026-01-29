using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SpotifyClone.Shared.BuildingBlocks.Application;
using SpotifyClone.Shared.BuildingBlocks.Application.Behaviors;
using SpotifyClone.Shared.BuildingBlocks.Application.Configuration;
using SpotifyClone.Shared.BuildingBlocks.Application.Email;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Messaging;

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
            cfg.AddOpenBehavior(typeof(TransactionalPipelineBehavior<,>));
        });

        services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SectionName));
        services.Configure<ApplicationSettings>(configuration.GetSection(ApplicationSettings.SectionName));

        services.AddScoped<IEmailSender, SmtpEmailSender>();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);

        return services;
    }
}
