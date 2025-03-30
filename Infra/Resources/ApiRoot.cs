using Infra.Resources.Hypermedia;

namespace Infra.Resources;

public class ApiRoot : HalResource
{
    public ApiRoot(ILinkService linkService) : base(linkService)
    {
        this.AddLink("ApiRoot", null, "self", "GET");
        this.AddLink("Slots", null, "slots", "GET");
        this.AddLink("Reservations", null, "reservations", "GET");
    }
}