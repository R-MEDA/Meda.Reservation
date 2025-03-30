namespace Infra.Resources.Hypermedia;

public interface ILinkService
{
    Link Generate(string endpointName, object? routeValues, string rel, string method);
}