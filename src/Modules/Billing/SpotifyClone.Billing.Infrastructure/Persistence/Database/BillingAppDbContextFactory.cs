using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SpotifyClone.Billing.Infrastructure.Persistence.Database;

internal sealed class BillingAppDbContextFactory
    : IDesignTimeDbContextFactory<BillingAppDbContext>
{
    public BillingAppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        string? connectionString = configuration.GetConnectionString("MainDb");

        var optionsBuilder = new DbContextOptionsBuilder<BillingAppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new BillingAppDbContext(optionsBuilder.Options);
    }
}
