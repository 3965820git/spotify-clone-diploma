using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Mappers;
using SpotifyClone.Shared.BuildingBlocks.Application.Errors;
using SpotifyClone.Shared.BuildingBlocks.Domain.Primitives;
using SpotifyClone.Shared.Kernel.Exceptions;
using SpotifyClone.Streaming.Domain.Aggregates.AudioAssets.Exceptions;
using SpotifyClone.Streaming.Domain.Aggregates.PlaybackSessions.Exceptions;
using SpotifyClone.Streaming.Domain.Exceptions;

namespace SpotifyClone.Streaming.Application.Errors;

public sealed class StreamingDomainExceptionMapper : IDomainExceptionMapper
{
    public Error MapToError(DomainExceptionBase domainException)
        => domainException switch
        {
            InvalidAudioFormatDomainException => MediaErrors.InvalidFormat,
            InvalidDurationDomainException => MediaErrors.InvalidDuration,
            InvalidImageMetadataDomainException => MediaErrors.InvalidImageMetadata,
            InvalidAudioAssetStatusDomainException => MediaErrors.InvalidStatus,
            TrackNotLinkedDomainException => MediaErrors.TrackNotLinkedToAudio,
            InvalidPlaybackContextDomainException => PlaybackErrors.InvalidPlaybackContext,
            EmptyPlaybackQueueDomainException => PlaybackErrors.EmptyQueue,
            InvalidDeviceDomainException => PlaybackErrors.InvalidDevice,
            _ => CommonErrors.Unknown
        };
}
