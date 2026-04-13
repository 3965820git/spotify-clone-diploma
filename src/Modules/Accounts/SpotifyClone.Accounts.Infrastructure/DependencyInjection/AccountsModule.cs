using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyClone.Accounts.Application;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Data;
using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Behaviors;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Application.Features.Auth.Commands.RegisterUser;
using SpotifyClone.Accounts.Application.Jobs;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Infrastructure.Persistence;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Repositories;
using SpotifyClone.Accounts.Infrastructure.Persistence.Auth;
using SpotifyClone.Accounts.Infrastructure.Persistence.Auth.Initialization;
using SpotifyClone.Accounts.Infrastructure.Persistence.Auth.Repositories;
using SpotifyClone.Accounts.Infrastructure.Persistence.Identity;
using SpotifyClone.Accounts.Infrastructure.Persistence.Identity.Database;
using SpotifyClone.Accounts.Infrastructure.Persistence.Identity.Services;
using SpotifyClone.Accounts.Infrastructure.Persistence.Queries;
using SpotifyClone.Accounts.Infrastructure.Services;
using SpotifyClone.Accounts.Infrastructure.Services.Jwt;
using SpotifyClone.Accounts.Infrastructure.Services.Sms;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Auth;

namespace SpotifyClone.Accounts.Infrastructure.DependencyInjection;

public static class AccountsModule
{
    public static IServiceCollection AddAccountsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            AccountsApplicationAssemblyReference.Assembly));

        services.AddValidatorsFromAssembly(AccountsApplicationAssemblyReference.Assembly);

        services.AddDbContext<AccountsAppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("MainDb"),
                b => b.MigrationsAssembly(typeof(AccountsAppDbContext).Assembly.FullName)));

        services.AddDbContext<IdentityAppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("MainDb"),
                b => b.MigrationsAssembly(typeof(IdentityAppDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultPhoneProvider;
            options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultPhoneProvider;
        })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<IdentityAppDbContext>()
            .AddDefaultTokenProviders();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "SpotifyClone_";
        });

        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromMinutes(15));

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IAccountsUnitOfWork>());
        services.AddScoped<IAccountsUnitOfWork, AccountsEfCoreUnitOfWork>();
        services.AddScoped<IUserProfileRepository, UserProfileEfCoreRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenEfCoreRepository>();
        services.AddScoped<IOutboxRepository, OutboxEfCoreRepository>();
        services.AddScoped<IUserReadService, UserEfCoreReadService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IOtpCacheService, OtpCacheService>();
        services.AddScoped<IDomainExceptionMapper, AccountsDomainExceptionMapper>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AccountsTransactionalPipelineBehavior<,>));
        services.AddTransient<ITokenHasher, Sha256TokenHasher>();
        services.AddTransient<ITokenService, JwtTokenService>();
        services.AddTransient<IAccountVerificationService, IdentityAccountVerificationService>();
        services.AddTransient<ISmsSender, LoggerSmsSender>();

        services.AddTransient<ProcessOutboxMessagesJob>();

        return services;
    }

    public async static Task UseAccountsModule(this IApplicationBuilder app)
    {
        await IdentitySeeder.SeedRolesAsync(app.ApplicationServices);

        using IServiceScope scope = app.ApplicationServices.CreateScope();
        UserManager<ApplicationUser> userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        ISender sender = scope.ServiceProvider
            .GetRequiredService<ISender>();

        const string adminEmail = "stopify@gmail.com";
        ApplicationUser? existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin is null)
        {
            await sender.Send(new RegisterUserCommand(
                Email: adminEmail,
                Password: "Admin_Password123",
                DisplayName: "Stopify",
                BirthDate: new DateTimeOffset(2007, 03, 04, 2, 0, 0, TimeSpan.Zero),
                Gender: "male",
                Role: UserRoles.Admin));
        }

        const string creator1Email = "stopifycreator@gmail.com";
        ApplicationUser? existingCreator1 = await userManager.FindByEmailAsync(creator1Email);
        if (existingCreator1 is null)
        {
            await sender.Send(new RegisterUserCommand(
                Email: creator1Email,
                Password: "Creator_Password123",
                DisplayName: "The Creator",
                BirthDate: new DateTimeOffset(new DateTime(2007, 03, 04, 2, 0, 0, DateTimeKind.Local)),
                Gender: "male",
                Role: UserRoles.Creator));
        }

        const string creator2Email = "stopifycreator2@gmail.com";
        ApplicationUser? existingCreator2 = await userManager.FindByEmailAsync(creator2Email);
        if (existingCreator2 is null)
        {
            await sender.Send(new RegisterUserCommand(
                Email: creator2Email,
                Password: "Creator_Password123",
                DisplayName: "The Creator 2",
                BirthDate: new DateTimeOffset(new DateTime(2007, 03, 04, 2, 0, 0, DateTimeKind.Local)),
                Gender: "male",
                Role: UserRoles.Creator));
        }

        IRecurringJobManager recurringJobManager =
            app.ApplicationServices.GetRequiredService<IRecurringJobManager>();
        recurringJobManager.AddOrUpdate<ProcessOutboxMessagesJob>(
            "accounts-outbox-processor",
            job => job.ProcessAsync(),
            "*/5 * * * * *" // Every 5 seconds (Cron expression)
        );
    }
}
