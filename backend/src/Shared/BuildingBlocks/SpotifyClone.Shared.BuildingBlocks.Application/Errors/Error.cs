namespace SpotifyClone.Shared.BuildingBlocks.Application.Errors;

public sealed record Error
{
    public string Code { get; }
    public string Description { get; }

    public Error(string code, string description)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public override string ToString()
        => $"{Code}: {Description}";
}
