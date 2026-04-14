using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SpotifyClone.Streaming.Infrastructure.Persistence.Database;

internal sealed class StreamingAppDbContextFactory
    : IDesignTimeDbContextFactory<StreamingAppDbContext>
{
    public StreamingAppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        string? connectionString = configuration.GetConnectionString("MainDb");

        var optionsBuilder = new DbContextOptionsBuilder<StreamingAppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new StreamingAppDbContext(optionsBuilder.Options);
    }
}
