using FluentAssertions;
using SpotifyClone.Shared.Kernel.Enums;
using SpotifyClone.Shared.Kernel.Exceptions;
using SpotifyClone.Shared.Kernel.ValueObjects;

namespace SpotifyClone.Shared.Kernel.Tests.ValueObjects;

public sealed class ImageMetadataTests
{
    [Fact]
    public void Constructor_Should_CreateInstance_When_ParametersAreValid()
    {
        // Arrange
        int width = 800;
        int height = 600;
        int maxWidth = 1920;
        int maxHeight = 1080;
        ImageFileType fileType = ImageFileType.Jpg;

        // Act
        var imageMetadata = new ImageMetadata(width, height, maxWidth, maxHeight, fileType);

        // Assert
        imageMetadata.Width.Should().Be(width);
        imageMetadata.Height.Should().Be(height);
        imageMetadata.FileType.Should().Be(ImageFileType.Jpg);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_Should_ThrowException_When_WidthIsInvalid(int invalidWidth)
    {
        // Arrange
        int height = 600;
        int maxWidth = 1920;
        int maxHeight = 1080;
        ImageFileType fileType = ImageFileType.Jpg;

        // Act
        Func<ImageMetadata> result = ()
            => new ImageMetadata(invalidWidth, height, maxWidth, maxHeight, fileType);

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_Should_ThrowException_When_HeightIsInvalid(int invalidHeight)
    {
        // Arrange
        int width = 600;
        int maxWidth = 1920;
        int maxHeight = 1080;
        ImageFileType fileType = ImageFileType.Jpg;

        // Act
        Func<ImageMetadata> result = ()
            => new ImageMetadata(width, invalidHeight, maxWidth, maxHeight, fileType);

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Constructor_Should_ThrowException_When_WidthIsTooLarge()
    {
        // Arrange
        int width = 1921;
        int height = 1080;
        int maxWidth = 1920;
        int maxHeight = 1080;
        ImageFileType fileType = ImageFileType.Jpg;

        // Act
        Func<ImageMetadata> result = ()
            => new ImageMetadata(width, height, maxWidth, maxHeight, fileType);

        // Assert
        result.Should().Throw<InvalidImageMetadataDomainException>();
    }

    [Fact]
    public void Constructor_Should_ThrowException_When_HeightIsTooLarge()
    {
        // Arrange
        int width = 600;
        int height = 1081;
        int maxWidth = 1920;
        int maxHeight = 1080;
        ImageFileType fileType = ImageFileType.Jpg;

        // Act
        Func<ImageMetadata> result = ()
            => new ImageMetadata(width, height, maxWidth, maxHeight, fileType);

        // Assert
        result.Should().Throw<InvalidImageMetadataDomainException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_Should_ThrowException_When_MaxWidthIsInvalid(int invalidMaxWidth)
    {
        // Arrange
        int width = 600;
        int height = 600;
        int maxHeight = 1920;
        ImageFileType fileType = ImageFileType.Jpg;

        // Act
        Func<ImageMetadata> result = ()
            => new ImageMetadata(width, height, invalidMaxWidth, maxHeight, fileType);

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_Should_ThrowException_When_MaxHeightIsInvalid(int invalidMaxHeight)
    {
        // Arrange
        int width = 600;
        int height = 600;
        int maxWidth = 1920;
        ImageFileType fileType = ImageFileType.Jpg;

        // Act
        Func<ImageMetadata> result = ()
            => new ImageMetadata(width, height, maxWidth, invalidMaxHeight, fileType);

        // Assert
        result.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void Constructor_Should_ThrowException_When_FileTypeIsNull()
    {
        // Arrange
        int width = 600;
        int height = 600;
        int maxWidth = 1920;
        int maxHeight = 1080;

        // Act
        Func<ImageMetadata> result = ()
            => new ImageMetadata(width, height, maxWidth, maxHeight, null!);

        // Assert
        result.Should().Throw<ArgumentNullException>();
    }
}
