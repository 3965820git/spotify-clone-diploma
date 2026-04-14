using System.Text.RegularExpressions;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Enums;

public sealed record Gender : ValueObject
{
    public static readonly Gender Male = new("male");
    public static readonly Gender Female = new("female");
    public static readonly Gender NonBinary = new("non_binary");
    public static readonly Gender SomethingElse = new("something_else");
    public static readonly Gender NotSpecified = new("not_specified");

    public string Value { get; }

    private Gender(string value)
        => Value = value;

    public static Gender From(string value)
    => Regex.Replace(value.Trim().ToLowerInvariant(), @"[^0-9A-Za-z]", string.Empty) switch
    {
        "male" => Male,
        "female" => Female,
        "nonbinary" => NonBinary,
        "somethingelse" => SomethingElse,
        "notspecified" => NotSpecified,
        _ => throw new InvalidGenderDomainException($"Gender {value} is not supported or invalid.")
    };
}
