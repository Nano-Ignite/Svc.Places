using System;
using System.IO;
using Nano.Storage.Abstractions;

namespace Svc.Places.Extensions;

internal static class PathProviderExtensions
{
    private const string ROOT_PLACES_PATH = "places";
    private const string ROOT_PLACES_OFFERS_PATH = "offers";

    internal static string GetPlacesPath(this IPathProvider pathProvider, Guid placeId)
    {
        ArgumentNullException.ThrowIfNull(pathProvider);

        return Path.Combine(pathProvider.Root, PathProviderExtensions.ROOT_PLACES_PATH, placeId.ToString());
    }

    internal static string GetPlaceOffersPath(this IPathProvider pathProvider, Guid placeId)
    {
        ArgumentNullException.ThrowIfNull(pathProvider);

        var placesPath = pathProvider
            .GetPlacesPath(placeId);

        return Path.Combine(placesPath, PathProviderExtensions.ROOT_PLACES_OFFERS_PATH);
    }
}