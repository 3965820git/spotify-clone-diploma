using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyClone.Accounts.Domain.Aggregates.Users;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Rules;
using SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations.Converters;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations;

internal sealed class UserProfileEfCoreConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasConversion(AccountsEfCoreValueConverters.UserIdConverter)
            .ValueGeneratedNever();

        builder.Property(x => x.DisplayName)
            .HasColumnName("display_name")
            .HasMaxLength(DisplayNameRules.MaxLength)
            .IsRequired();

        builder.Property(x => x.BirthDate)
            .HasColumnName("birth_date")
            .IsRequired();

        builder.Property(x => x.Gender)
            .HasColumnName("gender")
            .HasConversion(AccountsEfCoreValueConverters.GenderConverter)
            .HasMaxLength(20)
            .IsRequired();

        builder.OwnsOne(x => x.AvatarImage, avatar => avatar.Configure());

        builder.Navigation(x => x.AvatarImage)
            .IsRequired(false);

        builder.Ignore(u => u.DomainEvents);
    }
}
