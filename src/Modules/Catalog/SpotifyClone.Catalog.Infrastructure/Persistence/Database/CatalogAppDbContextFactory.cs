using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Database;

internal sealed class CatalogAppDbContextFactory
    : IDesignTimeDbContextFactory<CatalogAppDbContext>
{
    public CatalogAppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        string? connectionString = configuration.GetConnectionString("MainDb");

        var optionsBuilder = new DbContextOptionsBuilder<CatalogAppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new CatalogAppDbContext(optionsBuilder.Options);
    }
}
