using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;

public sealed record Gender : ValueObject
{
    public static readonly Gender Male = new("Male");
    public static readonly Gender Female = new("Female");
    public static readonly Gender NonBinary = new("NonBinary");
    public static readonly Gender NotSpecified = new("NotSpecified");

    private static readonly HashSet<string> Supported =
    [
        "Male",
        "Female",
        "NonBinary",
        "NotSpecified"
    ];

    public string Value { get; }

    private Gender(string value)
        => Value = value;

    public static Gender From(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        string normalized = value.Trim();

        if (!Supported.Contains(normalized))
        {
            throw new InvalidGenderDomainException($"Gender {normalized} is not supported.");
        }

        return new Gender(normalized);
    }
}
