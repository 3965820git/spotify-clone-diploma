using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Playlists.Domain.Aggregates.Playlists.Entities;
using SpotifyClone.Playlists.Infrastructure.Persistence.Configurations.Converters;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Configurations;

internal sealed class PlaylistTrackEfCoreConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
    public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
    {
        builder.ToTable("playlist_tracks");

        builder.Property("Id")
            .HasColumnName("id");
        builder.HasKey("Id");

        builder.Property(x => x.Id)
            .HasColumnName("track_id")
            .HasConversion(PlaylistsEfCoreValueConverters.TrackIdConverter)
            .IsRequired();

        builder.Property(x => x.PlaylistId)
            .HasColumnName("playlist_id")
            .HasConversion(PlaylistsEfCoreValueConverters.PlaylistIdConverter)
            .IsRequired();

        builder.Property(x => x.CreatorId)
            .HasColumnName("creator_id")
            .HasConversion(PlaylistsEfCoreValueConverters.UserIdConverter)
            .IsRequired();

        builder.Property(x => x.Position)
            .HasColumnName("position")
            .IsRequired();
        builder.HasIndex(nameof(PlaylistTrack.PlaylistId), nameof(PlaylistTrack.Position))
            .IsUnique();

        builder.HasIndex(nameof(PlaylistTrack.Id), nameof(PlaylistTrack.PlaylistId)).IsUnique();
    }
}
