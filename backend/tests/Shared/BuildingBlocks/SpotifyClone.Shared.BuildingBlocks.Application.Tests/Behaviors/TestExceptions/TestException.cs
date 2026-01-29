namespace SpotifyClone.Shared.BuildingBlocks.Application.Tests.Behaviors.TestExceptions;

internal sealed class TestException(string message)
    : Exception(message)
{
}
