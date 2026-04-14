using FluentAssertions;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;

namespace SpotifyClone.Shared.BuildingBlocks.Application.Tests.Errors;

public sealed class ErrorTests
{
    [Fact]
    public void Constructor_Should_AssignData()
    {
        // Arrange
        string expectedCode = "Test";
        string expectedDescription = "This error is just for test purposes.";

        // Act
        var error = new Error(expectedCode, expectedDescription);

        // Assert
        error.Code.Should().Be(expectedCode);
        error.Description.Should().Be(expectedDescription);
    }

    [Fact]
    public void Code_Should_NotBeNull()
    {
        // Arrange
        string description = "This error is just for test purposes.";

        // Act
        Action act = () => new Error(null!, description);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("code");
    }

    [Fact]
    public void Description_Should_NotBeNull()
    {
        // Arrange
        string code = "Test";

        // Act
        Action act = () => new Error(code, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("description");
    }

    [Fact]
    public void ToString_Should_ReturnFormattedString()
    {
        // Arrange
        string expectedCode = "Test";
        string expectedDescription = "This error is just for test purposes.";
        var error = new Error(expectedCode, expectedDescription);
        string expectedString = $"{expectedCode}: {expectedDescription}";

        // Act
        string result = error.ToString();

        // Assert
        result.Should().Be(expectedString);
    }

    [Fact]
    public void ErrorsWithEqualData_Should_BeEqual()
    {
        // Arrange
        string expectedCode = "Test";
        string expectedDescription = "This error is just for test purposes.";
        var error1 = new Error(expectedCode, expectedDescription);
        var error2 = new Error(expectedCode, expectedDescription);

        // Act
        bool areEqual = error1 == error2;

        // Assert
        areEqual.Should().BeTrue();
    }

    [Fact]
    public void ErrorsWithDifferentData_Should_NotBeEqual()
    {
        // Arrange
        var error1 = new Error("Test1", "Test error 1");
        var error2 = new Error("Test2", "Test error 2");

        // Act
        bool areEqual = error1 == error2;

        // Assert
        areEqual.Should().BeFalse();
    }
}
