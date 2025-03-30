// using Microsoft.AspNetCore.Mvc;
// using Infra.Resources;

// namespace Infra.Controllers;

// [ApiController]
// [Route("api")]
// public class DiscoveryController : ControllerBase
// {
//     [HttpGet]
//     public ActionResult<HalResource> GetApiRoot()
//     {
//         LinkGenerator linkGenerator;

//         linkGenerator.GetUriByName(ContextBoundObject,)

//         var resource = new HalResource();
//         var baseUrl = $"{Request.Scheme}://{Request.Host}";

//         resource.AddLink("self", new Link(Url.Action(nameof(GetApiRoot), "Schedule")!));
//         resource.AddLink("slots", new Link(Url.Action(nameof(ScheduleController.GetAll), "Schedule")!));
//         resource.AddLink("bookings", new Link($"/api/reservations"));

//         return Ok(resource);
//     }
// }
