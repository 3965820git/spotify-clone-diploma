namespace SpotifyClone.Accounts.Application.Abstractions.Services;

public interface ITokenHasher
{
    string Hash(string rawToken);
    bool Verify(string rawToken, string hash);
}
