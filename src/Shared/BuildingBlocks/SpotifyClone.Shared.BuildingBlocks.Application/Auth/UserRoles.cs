namespace SpotifyClone.Shared.BuildingBlocks.Application.Auth;

public static class UserRoles
{
    public const string Listener = "listener";
    public const string Creator = "creator";
    public const string Admin = "admin";

    public static readonly IEnumerable<string> AllRoles = [ Listener, Creator, Admin ];

    public static string[] CalculateBy(string role)
        => role switch
        {
            Admin => [ Admin, Creator, Listener ],
            Creator => [ Creator, Listener ],
            Listener => [ Listener ],
            _ => throw new ArgumentException($"Invalid role {role}")
        };
}
