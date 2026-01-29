using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using SpotifyClone.Shared.BuildingBlocks.Application.Email;
using SpotifyClone.Shared.BuildingBlocks.Infrastructure.Messaging;

namespace SpotifyClone.Shared.BuildingBlocks.Infrastructure.Tests.Messaging;

public sealed class SmtpEmailSenderTests
{
    private readonly Mock<IOptions<SmtpOptions>> _optionsMock = new();

    [Fact]
    public void SendAsync_Should_NotThrowForValidMessage()
    {
        // Arrange
        _optionsMock.Setup(o => o.Value).Returns(new SmtpOptions
        {
            Host = "Host",
            Port = 1234,
            EnableSsl = false
        });
        var sender = new SmtpEmailSender(_optionsMock.Object);
        var message = new EmailMessage(["test@example.com"], "Test");

        // Act
        Func<object> act = () => sender.SendAsync(message);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public async Task SendAsync_ShouldHonorCancellation()
    {
        // Arrange
        _optionsMock.Setup(o => o.Value).Returns(new SmtpOptions
        {
            Host = "Host",
            Port = 1234,
            EnableSsl = false
        });
        var sender = new SmtpEmailSender(_optionsMock.Object);
        var message = new EmailMessage(["test@example.com"], "Test");

        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        // Act
        Func<Task> act = () => sender.SendAsync(message, cancellationToken: cts.Token);

        // Assert
        await act.Should().ThrowAsync<OperationCanceledException>();
    }
}
