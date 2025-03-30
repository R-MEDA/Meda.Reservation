using System.Text.Json.Serialization;

namespace Infra.Resources.Hypermedia;

public abstract class HalResource
{
    private readonly ILinkService _linkService;

    [JsonPropertyName("_links")]
    protected List<Link> Links { get; private set; } = [];

    protected HalResource(ILinkService linkService)
    {
        _linkService = linkService;
    }

    protected void AddLink(string endpointName, object? routeValues, string rel, string method)
    {
        Links.Add(_linkService.Generate(endpointName, routeValues, rel, method));
    }
}