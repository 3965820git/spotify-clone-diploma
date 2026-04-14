using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Domain.Aggregates.Users.ValueObjects;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations.Converters;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations;

internal static class AvatarImageEfCoreConfiguration
{
    public static OwnedNavigationBuilder<UserProfile, AvatarImage> Configure(
        this OwnedNavigationBuilder<UserProfile, AvatarImage> builder)
    {
        builder.WithOwner();

        builder.Property(a => a.ImageId)
            .HasConversion(AccountsEfCoreValueConverters.ImageIdConverter)
            .HasColumnName("avatar_image_id");

        builder.OwnsOne(a => a.Metadata, metadata => metadata.Configure());

        builder.HasIndex(a => a.ImageId)
            .IsUnique();

        return builder;
    }
}
