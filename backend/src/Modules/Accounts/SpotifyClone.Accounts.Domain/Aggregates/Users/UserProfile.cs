using SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Events;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Rules;
using SpotifyClone.Accounts.Domain.Aggregates.Users.ValueObjects;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users;

public sealed class UserProfile : AggregateRoot<UserId, Guid>
{
    public string DisplayName { get; private set; }
    public DateTimeOffset BirthDate { get; private set; }
    public Gender Gender { get; private set; }
    public AvatarImage? AvatarImage { get; private set; }

    private UserProfile()
    {
        DisplayName = null!;
        Gender = null!;
    }

    private UserProfile(
        UserId id, string displayName, DateTimeOffset birthDate, Gender gender, AvatarImage? avatarImage = null)
        : base(id)
    {
        DisplayName = displayName;
        BirthDate = birthDate;
        Gender = gender;
        AvatarImage = avatarImage;
    }

    public static UserProfile Create(
        UserId id, string displayName, DateTimeOffset birthDate, Gender gender)
    {
        DisplayNameRules.Validate(displayName);
        BirthDateRules.Validate(birthDate);

        return new UserProfile(id, displayName, birthDate, gender);
    }

    public void ProcessRegistration(string email, string confirmationToken)
        => RaiseDomainEvent(new UserRegisteredDomainEvent(
            Id.Value,
            email,
            confirmationToken));
}
