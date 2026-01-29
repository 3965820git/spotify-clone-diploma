using FluentAssertions;
using SpotifyClone.Accounts.Domain.Aggregates.Users.Exceptions;
using SpotifyClone.Accounts.Domain.Aggregates.Users.ValueObjects;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.IDs;

namespace SpotifyClone.Accounts.Domain.Tests.Aggregates.Users.ValueObjects;

public sealed class AvatarImageTests
{
    [Fact]
    public void Constructor_Should_CreateInstance_When_ParametersAreValid()
    {
        // Arrange
        var imageId = ImageId.New();
        int width = 512;
        int height = 512;
        ImageFileType fileType = ImageFileType.Png;

        // Act
        var avatarImage = new AvatarImage(imageId, width, height, fileType);

        // Assert
        avatarImage.ImageId.Should().Be(imageId);
        avatarImage.Metadata.Width.Should().Be(width);
        avatarImage.Metadata.Height.Should().Be(height);
        avatarImage.Metadata.FileType.Should().Be(fileType);
    }

    [Fact]
    public void Constructor_Should_ThrowException_When_ImageIdIsNull()
    {
        // Arrange
        ImageId imageId = null!;
        int width = 512;
        int height = 512;
        ImageFileType fileType = ImageFileType.Png;

        // Act
        Action act = () => new AvatarImage(imageId, width, height, fileType);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_Should_ThrowException_When_FileTypeIsNull()
    {
        // Arrange
        var imageId = ImageId.New();
        int width = 512;
        int height = 512;
        ImageFileType fileType = null!;

        // Act
        Action act = () => new AvatarImage(imageId, width, height, fileType);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Constructor_Should_ThrowException_When_WidthAndHeightAreNotEqual()
    {
        // Arrange
        var imageId = ImageId.New();
        int width = 512;
        int height = 256;
        ImageFileType fileType = ImageFileType.Png;

        // Act
        Action act = () => new AvatarImage(imageId, width, height, fileType);

        // Assert
        act.Should().Throw<InvalidAvatarImageDomainException>();
    }

    [Fact]
    public void Constructor_Should_ThrowException_When_FileTypeDoesNotSupportTransparency()
    {
        // Arrange
        var imageId = ImageId.New();
        int width = 512;
        int height = 512;
        ImageFileType fileType = ImageFileType.Jpeg;

        // Act
        Action act = () => new AvatarImage(imageId, width, height, fileType);

        // Assert
        act.Should().Throw<InvalidAvatarImageDomainException>();
    }
}
