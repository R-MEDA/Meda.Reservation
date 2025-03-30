using System.Text.Json.Serialization;

namespace Infra.Resources.Hypermedia;

public abstract class HalResource
{
    private readonly ILinkService _linkService;

    [JsonPropertyName("_links")]
    public List<Link> Links { get; set; } = [];

    public HalResource(ILinkService linkService)
    {
        _linkService = linkService;
    }

    public void AddLink(string endpointName, object? routeValues, string rel, string method)
    {
        Links.Add(_linkService.Generate(endpointName, routeValues, rel, method));
    }
}