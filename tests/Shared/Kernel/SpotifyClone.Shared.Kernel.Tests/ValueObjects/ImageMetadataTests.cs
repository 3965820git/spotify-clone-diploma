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
        ImageFileType fileType = ImageFileType.Jpg;
        long sizeInBytes = 150000;

        // Act
        var imageMetadata = new ImageMetadata(width, height, fileType, sizeInBytes);

        // Assert
        imageMetadata.Width.Should().Be(width);
        imageMetadata.Height.Should().Be(height);
        imageMetadata.FileType.Should().Be(ImageFileType.Jpg);
    }
}
