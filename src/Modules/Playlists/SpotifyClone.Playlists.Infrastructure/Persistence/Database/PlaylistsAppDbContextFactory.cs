using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Database;

internal sealed class PlaylistsAppDbContextFactory
    : IDesignTimeDbContextFactory<PlaylistsAppDbContext>
{
    public PlaylistsAppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        string? connectionString = configuration.GetConnectionString("MainDb");

        var optionsBuilder = new DbContextOptionsBuilder<PlaylistsAppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PlaylistsAppDbContext(optionsBuilder.Options);
    }
}
