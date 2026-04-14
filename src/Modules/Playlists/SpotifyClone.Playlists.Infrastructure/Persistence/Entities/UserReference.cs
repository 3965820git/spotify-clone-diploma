namespace SpotifyClone.Playlists.Infrastructure.Persistence.Entities;

public sealed class UserReference
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid? AvatarImageId { get; set; }

    public UserReference(Guid id, string name, Guid? avatarImageId)
    {
        Id = id;
        Name = name;
        AvatarImageId = avatarImageId;
    }

    private UserReference()
    {
    }
}
