using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.Entities;
using SpotifyClone.Catalog.Domain.Aggregates.Albums.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Configurations.Converters;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class AlbumTrackEfCoreConfiguration : IEntityTypeConfiguration<AlbumTrack>
{
    public void Configure(EntityTypeBuilder<AlbumTrack> builder)
    {
        builder.ToTable("album_tracks");

        builder.Property<AlbumId>("album_id")
            .HasConversion(CatalogEfCoreValueConverters.AlbumIdConverter);

        builder.Property(x => x.Id)
            .HasColumnName("track_id")
            .HasConversion(CatalogEfCoreValueConverters.TrackIdConverter)
            .IsRequired();

        builder.HasKey("album_id", nameof(AlbumTrack.Id));

        builder.Property(x => x.Position)
            .HasColumnName("position")
            .IsRequired();

        builder.HasIndex(nameof(AlbumTrack.Id), "album_id").IsUnique();
        builder.HasIndex(nameof(AlbumTrack.Id), nameof(AlbumTrack.Position), "album_id").IsUnique();
    }
}
