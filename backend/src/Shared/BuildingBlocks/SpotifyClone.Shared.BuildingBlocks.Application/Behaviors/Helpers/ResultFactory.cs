using System.Reflection;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Behaviors.Helpers;

internal static class ResultFactory
{
    public static TResult CreateFailureResult<TResult>(params Error[] errors)
    {
        Type responseType = typeof(TResult);
        object? failureResult;

        MethodInfo[] methods = typeof(Result).GetMethods(BindingFlags.Public | BindingFlags.Static);

        MethodInfo[] failureMethods = methods
            .Where(m => m.Name == "Failure" &&
                        m.GetParameters() is { Length: 1 } p &&
                        p[0].ParameterType == typeof(Error[]))
            .ToArray();

        if (failureMethods.Length != 2)
        {
            throw new InvalidOperationException("Expected exactly two Failure method overloads on Result.");
        }

        MethodInfo? nonGenericMethod = failureMethods.FirstOrDefault(m => !m.IsGenericMethod);
        MethodInfo? genericMethodDef = failureMethods.FirstOrDefault(m => m.IsGenericMethod);

        if (nonGenericMethod == null || genericMethodDef == null)
        {
            throw new InvalidOperationException("Failure method overloads not found on Result.");
        }

        if (responseType == typeof(Result))
        {
            failureResult = nonGenericMethod.Invoke(null, [errors]);
        }
        else if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type valueType = responseType.GetGenericArguments()[0];
            MethodInfo closedMethod = genericMethodDef.MakeGenericMethod(valueType);
            failureResult = closedMethod.Invoke(null, [errors]);
        }
        else
        {
            throw new InvalidOperationException($"Unexpected response type: {responseType}");
        }

        if (failureResult == null)
        {
            throw new InvalidOperationException("Failed to invoke Failure method.");
        }

        return (TResult)failureResult;
    }
}
