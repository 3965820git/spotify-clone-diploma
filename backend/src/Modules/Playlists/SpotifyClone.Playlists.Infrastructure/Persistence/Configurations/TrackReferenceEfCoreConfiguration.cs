using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Playlists.Infrastructure.Persistence.Configurations.Converters;
using SpotifyClone.Playlists.Infrastructure.Persistence.Entities;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Configurations;

internal sealed class TrackReferenceEfCoreConfiguration
    : IEntityTypeConfiguration<TrackReference>
{
    public void Configure(EntityTypeBuilder<TrackReference> builder)
    {
        builder.ToTable("track_references");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion(PlaylistsEfCoreValueConverters.CatalogTrackStatusConverter)
            .IsRequired();

        builder.Property(x => x.CoverImageId)
            .HasColumnName("cover_image_id")
            .IsRequired(false);

        builder.HasKey(x => x.Id);
    }
}
