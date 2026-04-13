using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Playlists.Infrastructure.Persistence.Entities;

namespace SpotifyClone.Playlists.Infrastructure.Persistence.Configurations;

internal sealed class UserReferenceEfCoreConfiguration
    : IEntityTypeConfiguration<UserReference>
{
    public void Configure(EntityTypeBuilder<UserReference> builder)
    {
        builder.ToTable("user_references");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(x => x.AvatarImageId)
            .HasColumnName("avatar_image_id")
            .IsRequired(false);

        builder.HasKey(x => x.Id);
    }
}
