using System.Reflection;
using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Services;

public static class IntegrationEventTypeRegistry
{
    private static readonly Dictionary<string, Type> _typeMap = new Dictionary<string, Type>();

    static IntegrationEventTypeRegistry()
    {
        var integrationEventsAssembly = Assembly.Load("SpotifyClone.Shared.IntegrationEvents");

        IEnumerable<Type> eventTypes = integrationEventsAssembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(IntegrationEvent)) && !t.IsAbstract);

        foreach (Type type in eventTypes)
        {
            string key = type.Name;
            _typeMap.TryAdd(key, type);
        }
    }

    public static Type? GetTypeFromName(string name) =>
        _typeMap.TryGetValue(name, out Type? type) ? type : null;

    public static string GetKeyForType(Type type) =>
        type.Name;
}
