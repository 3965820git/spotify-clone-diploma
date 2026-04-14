using Microsoft.EntityFrameworkCore;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Entities;
using SpotifyClone.Playlists.Infrastructure.Persistence.Entities;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Database;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Database;

public sealed class PlaylistsAppDbContext(DbContextOptions<PlaylistsAppDbContext> options)
    : ApplicationDbContext<PlaylistsAppDbContext>("playlists", options)
{
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistTrack> PlaylistTracks => Set<PlaylistTrack>();
    public DbSet<TrackReference> TrackReferences => Set<TrackReference>();
    public DbSet<UserReference> UserReferences => Set<UserReference>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlaylistsAppDbContext).Assembly);
    }
}
