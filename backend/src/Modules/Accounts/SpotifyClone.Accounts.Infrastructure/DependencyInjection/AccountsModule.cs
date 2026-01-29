using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SpotifyClone.Accounts.Application;
using SpotifyClone.Accounts.Application.Abstractions;
using SpotifyClone.Accounts.Application.Abstractions.Repositories;
using SpotifyClone.Accounts.Application.Abstractions.Services;
using SpotifyClone.Accounts.Application.Abstractions.Services.Models;
using SpotifyClone.Accounts.Application.Errors;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Infrastructure.Auth;
using SpotifyClone.Accounts.Infrastructure.Auth.Jwt;
using SpotifyClone.Accounts.Infrastructure.Auth.Sms;
using SpotifyClone.Accounts.Infrastructure.Persistence;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Repositories;
using SpotifyClone.Accounts.Infrastructure.Persistence.Auth;
using SpotifyClone.Accounts.Infrastructure.Persistence.Auth.Repositories;
using SpotifyClone.Accounts.Infrastructure.Persistence.Identity;
using SpotifyClone.Accounts.Infrastructure.Persistence.Identity.Database;
using SpotifyClone.Accounts.Infrastructure.Persistence.Identity.Services;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;

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
            options.User.RequireUniqueEmail = true;
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultPhoneProvider;
        })
            .AddEntityFrameworkStores<IdentityAppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.SectionName));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IAccountsUnitOfWork>());
        services.AddScoped<IAccountsUnitOfWork, AccountsEfCoreUnitOfWork>();
        services.AddScoped<IUserProfileRepository, UserProfileEfCoreRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenEfCoreRepository>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddTransient<IDomainExceptionMapper, AccountsDomainExceptionMapper>();
        services.AddTransient<ITokenHasher, Sha256TokenHasher>();
        services.AddTransient<ITokenService, JwtTokenService>();
        services.AddTransient<ICurrentUser, CurrentUser>();
        services.AddTransient<IAccountVerificationService, IdentityAccountVerificationService>();
        services.AddTransient<ISmsSender, LoggerSmsSender>();

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<AuthOptions>>().Value);

        return services;
    }
}
