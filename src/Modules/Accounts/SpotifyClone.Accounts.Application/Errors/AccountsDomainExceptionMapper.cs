using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Application.Errors;

public sealed class AccountsDomainExceptionMapper : IDomainExceptionMapper
{
    public Error MapToError(DomainExceptionBase domainException)
        => domainException switch
        {
            InvalidAvatarImageDomainException => UserProfileErrors.InvalidAvatarImage,
            InvalidBirthDateDomainException => UserProfileErrors.InvalidBirthDate,
            InvalidDisplayNameDomainException => UserProfileErrors.InvalidDisplayName,
            InvalidGenderDomainException => UserProfileErrors.InvalidGender,
            _ => CommonErrors.Unknown
        };
}
