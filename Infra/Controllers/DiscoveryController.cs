using Microsoft.AspNetCore.Mvc;
using Infra.Resources.Hypermedia;
using Infra.Resources;

namespace Infra.Controllers;

[ApiController]
[Route("api")]
public class DiscoveryController : ControllerBase
{
    private readonly ILinkService _linkService;

    public DiscoveryController(ILinkService linkService)
    {
        _linkService = linkService;
    }

    [HttpGet(Name = "ApiRoot")]
    public ActionResult<ApiRoot> GetApiRoot()
    {
        var resource = new ApiRoot(_linkService);

        return Ok(resource);
    }
}
