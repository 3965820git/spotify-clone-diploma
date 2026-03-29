namespace SpotifyClone.Shared.BuildingBlocks.Domain.Extensions;

public static class EnumExtensions
{
    public static T Next<T>(this T src) where T : struct, Enum
    {
        T[] values = Enum.GetValues<T>();
        int j = Array.IndexOf(values, src) + 1;
        return (values.Length == j) ? values[0] : values[j];
    }
}
