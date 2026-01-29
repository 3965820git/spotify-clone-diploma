using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Persistence.Converters;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Accounts.Configurations.Converters;

internal static class AccountsEfCoreValueConverters
{
    public static readonly StronglyTypedIdEfCoreConverter<UserId, Guid> UserIdConverter = new(
        v => UserId.From(v));

    public static readonly ValueConverter<Gender, string> GenderConverter = new(
        g => g.Value,
        v => Gender.From(v));

    public static readonly StronglyTypedIdEfCoreConverter<ImageId, Guid> ImageIdConverter = new(
        v => ImageId.From(v));

    public static readonly ValueConverter<ImageFileType, string> ImageFileTypeConverter = new(
        ft => ft.Value,
        v => ImageFileType.From(v));
}
