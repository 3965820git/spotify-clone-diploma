using Microsoft.EntityFrameworkCore;
using SpotifyClone.Catalog.Application.Abstractions.Data;
using SpotifyClone.Catalog.Application.Features.Genres.Queries;
using SpotifyClone.Catalog.Application.Models;
using SpotifyClone.Catalog.Domain.Aggregates.Genres.ValueObjects;
using SpotifyClone.Catalog.Infrastructure.Persistence.Database;

namespace SpotifyClone.Catalog.Infrastructure.Persistence.Queries;

internal sealed class GenreEfCoreReadService(
    CatalogAppDbContext context)
    : IGenreReadService
{
    private readonly CatalogAppDbContext _context = context;

    public async Task<bool> ExistsAsync(
        GenreId id,
        CancellationToken cancellationToken = default)
        => await _context.Genres
        .AnyAsync(g => g.Id == id, cancellationToken);

    public async Task<GenreDetails?> GetDetailsAsync(
        GenreId id,
        CancellationToken cancellationToken = default)
        => await _context.Genres
        .AsNoTracking()
        .Where(g => g.Id == id)
        .Select(g => new GenreDetails(
            g.Id.Value,
            g.Name,
            g.Cover == null ? null : new ImageMetadataDetails(
                g.Cover.ImageId.Value,
                g.Cover.Metadata.Width,
                g.Cover.Metadata.Height,
                g.Cover.Metadata.FileType.Value,
                g.Cover.Metadata.SizeInBytes)))
        .SingleOrDefaultAsync(cancellationToken);
}
