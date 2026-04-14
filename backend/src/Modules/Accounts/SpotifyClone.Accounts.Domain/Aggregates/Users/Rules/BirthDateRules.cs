using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;

namespace SpotifyClone.Accounts.Domain.Aggregates.Users.Rules;

public static class BirthDateRules
{
    public const short MinimumAge = 13;
    public const short MaximumAge = 120;

    public static void Validate(DateTimeOffset birthDateUniversal)
    {
        if (birthDateUniversal > DateTimeOffset.UtcNow)
        {
            throw new InvalidBirthDateDomainException("Birth date cannot be in the future.");
        }

        int age = CalculateAge(birthDateUniversal);

        if (age < MinimumAge)
        {
            throw new InvalidBirthDateDomainException($"User must be at least {MinimumAge} years old.");
        }

        if (age > MaximumAge)
        {
            throw new InvalidBirthDateDomainException($"User cannot be older than {MaximumAge} years.");
        }
    }

    private static int CalculateAge(DateTimeOffset birthDate)
    {
        int age = DateTimeOffset.UtcNow.Year - birthDate.Year;

        if (birthDate.Date > DateTimeOffset.UtcNow.Date.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
