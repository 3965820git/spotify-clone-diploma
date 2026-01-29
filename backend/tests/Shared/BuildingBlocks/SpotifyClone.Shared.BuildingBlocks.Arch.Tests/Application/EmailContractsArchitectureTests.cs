using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Application.Email;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Application;

public sealed class EmailContractsArchitectureTests
{
    [Fact]
    public void EmailPriority_Should_Have_Expected_Members()
    {
        string[] values = Enum.GetNames<EmailPriority>();
        values.Should().BeEquivalentTo("Low", "Normal", "High");
    }
}
