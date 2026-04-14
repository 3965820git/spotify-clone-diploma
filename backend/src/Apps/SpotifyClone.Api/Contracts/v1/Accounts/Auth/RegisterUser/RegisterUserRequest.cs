using System.ComponentModel.DataAnnotations;

namespace SpotifyClone.Api.Contracts.v1.Accounts.Auth.RegisterUser;

public sealed record RegisterUserRequest
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string DisplayName { get; set; }
    public required DateTimeOffset BirthDate { get; set; } // 2005-04-25T17:07:17
    public required string Gender { get; set; }
}
