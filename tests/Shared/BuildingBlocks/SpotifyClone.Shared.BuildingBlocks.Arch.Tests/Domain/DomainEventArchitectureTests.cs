using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;

namespace SpotifyClone.Shared.BuildingBlocks.Arch.Tests.Domain;

public sealed class DomainEventArchitectureTests
{
    [Fact]
    public void DomainEvent_Should_BeAbstractClass()
    {
        // Arrange
        Type domainEventType = typeof(DomainEvent);

        // Act
        bool isAbstractClass = domainEventType.IsAbstract && domainEventType.IsClass;

        // Assert
        isAbstractClass.Should().BeTrue();
    }

    [Fact]
    public void DomainEvent_Should_ImplementINotificationInterface()
    {
        // Arrange
        Type domainEventType = typeof(DomainEvent);
        var iNotificationType = Type.GetType("MediatR.INotification, MediatR");

        // Act
        bool implementsINotification = iNotificationType != null && iNotificationType.IsAssignableFrom(domainEventType);
        
        // Assert
        implementsINotification.Should().BeTrue();
    }

    // This method will be moved to different test modules once we have some REAL code that implements DomainEvent.
    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        // Arrange
        Assembly domainAssembly = typeof(DomainEvent).Assembly;

        // Act
        TestResult result = Types.InAssembly(domainAssembly)
            .That()
            .Inherit(typeof(DomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    // This method will be moved to different test modules once we have some REAL code that implements DomainEvent.
    [Fact]
    public void DomainEvents_Should_HaveDomainEventPostfix()
    {
        // Arrange
        Assembly domainAssembly = typeof(DomainEvent).Assembly;

        // Act
        TestResult result = Types.InAssembly(domainAssembly)
            .That()
            .Inherit(typeof(DomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
