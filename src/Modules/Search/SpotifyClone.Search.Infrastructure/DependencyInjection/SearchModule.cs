using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SpotifyClone.Search.Application;
using SpotifyClone.Search.Application.Abstractions.Services;
using SpotifyClone.Search.Infrastructure.Services;

namespace SpotifyClone.Search.Infrastructure.DependencyInjection;

public static class SearchModule
{
    public static IServiceCollection AddSearchModule(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
            SearchApplicationAssemblyReference.Assembly));

        services.AddValidatorsFromAssembly(SearchApplicationAssemblyReference.Assembly);

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MeiliSearchOptions>>().Value);
        services.AddSingleton<ISearchIndexer, MeiliSearchService>();
        services.AddSingleton<ISearchProvider, MeiliSearchService>();

        //services.AddScoped<IPlaylistReadService, PlaylistEfCoreReadService>();

        //services.AddScoped<IDomainExceptionMapper, SearchDomainExceptionMapper>();

        //services.AddTransient<ProcessOutboxMessagesJob>();

        return services;
    }
}
