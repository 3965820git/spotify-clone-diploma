using System.Security.Cryptography;
using System.Text;
using SpotifyClone.Accounts.Application.Abstractions.Services;

namespace SpotifyClone.Accounts.Infrastructure.Persistence.Auth;

internal class Sha256TokenHasher : ITokenHasher
{
    public string Hash(string rawToken)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(rawToken);
        return Convert.ToBase64String(SHA256.HashData(bytes));
    }

    public bool Verify(string rawToken, string hash)
        => Hash(rawToken) == hash;
}
