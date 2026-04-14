using SpotifyClone.Shared.BuildingBlocks.Application.Abstractions.Queries;
using SpotifyClone.Shared.BuildingBlocks.Application.Results;
using SpotifyClone.Streaming.Application.Abstractions.Services;

namespace SpotifyClone.Streaming.Application.Features.Media.Queries.GetImageAsset;

internal sealed class GetImageAssetQueryHandler(
    IFileStorage storage)
    : IQueryHandler<GetImageAssetQuery, GetImageAssetQueryResult>
{
    private readonly IFileStorage _storage = storage;

    public async Task<Result<GetImageAssetQueryResult>> Handle(
        GetImageAssetQuery request,
        CancellationToken cancellationToken)
    {
        string baseUrl = _storage.GetImageRootPath();
        string imageUrl = $"{baseUrl}/{request.ImageId}/image.webp";

        return new GetImageAssetQueryResult(request.ImageId, imageUrl);
    }
}
