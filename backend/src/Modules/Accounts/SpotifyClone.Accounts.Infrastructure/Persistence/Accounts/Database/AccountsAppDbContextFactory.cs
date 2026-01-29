using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Database;

internal sealed class AccountsAppDbContextFactory
    : IDesignTimeDbContextFactory<AccountsAppDbContext>
{
    public AccountsAppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        string? connectionString = configuration.GetConnectionString("MainDb");

        var optionsBuilder = new DbContextOptionsBuilder<AccountsAppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AccountsAppDbContext(optionsBuilder.Options);
    }
}
