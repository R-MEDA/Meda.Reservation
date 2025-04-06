using Infra.Resources.Hypermedia;

namespace Infra.Resources;

public class ApiRoot : HalResource
{
    public ApiRoot(ILinkService linkService) : base(linkService)
    {
        AddLink("ApiRoot", null, "self", "GET");
        AddLink("Slots", null, "slots", "GET");
        AddLink("Reservations", null, "reservations", "GET");
    }
}