using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Accounts.Domain.Aggregates.Users.ValueObjects;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations.Converters;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations;

internal static class ImageMetadataEfcoreConfiguration
{
    public static OwnedNavigationBuilder<AvatarImage, ImageMetadata> Configure(
        this OwnedNavigationBuilder<AvatarImage, ImageMetadata> builder)
    {
        builder.Property(m => m.Width)
            .HasColumnName("avatar_width")
            .HasMaxLength(AvatarImage.MaxWidth)
            .IsRequired();

        builder.Property(m => m.Height)
            .HasColumnName("avatar_height")
            .HasMaxLength (AvatarImage.MaxHeight)
            .IsRequired();

        builder.Property(m => m.FileType)
            .HasConversion(AccountsEfCoreValueConverters.ImageFileTypeConverter)
            .HasColumnName("avatar_file_type")
            .HasMaxLength(10)
            .IsRequired();

        return builder;
    }
}

