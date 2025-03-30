using Microsoft.AspNetCore.Mvc;
using Domain.Ports;
using Infra.Resources;
using Domain.TimeSlot;
using Domain.Exceptions;
using Domain.UseCases;
using Infra.Resources.Hypermedia;

namespace Infra.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ITimeSlots _timeslotRepository;
    private readonly IBookTimeSlot _bookTimeSlot;
    private readonly ILinkService _linkService;

    public ScheduleController(
        ITimeSlots timeslotRepository,
        IBookTimeSlot createBooking,
        ILinkService linkService
    )
    {
        _timeslotRepository = timeslotRepository;
        _bookTimeSlot = createBooking;
        _linkService = linkService;
    }

    [HttpGet("slots/{id}", Name = "GetSlotById")]
    public async Task<ActionResult<TimeSlotResource>> GetById(Guid id)
    {
        TimeSlot timeslot = await _timeslotRepository.GetById(id) ?? throw new TimeSlotNotFoundException(id);

        if (timeslot == null)
        {
            return NotFound(id);
        }

        return Ok(new TimeSlotResource(timeslot, _linkService));
    }

    [HttpGet(Name = "Slots")]
    public async Task<ActionResult<TimeSlotResource>> GetAll()
    {
        var timeslots = await _timeslotRepository.GetAll();
        var resources = timeslots.Select(t => new TimeSlotResource(t, _linkService));
        return Ok(resources);
    }

    [HttpPost("slots/{slotId}/book", Name = "BookSlot")]
    public async Task<ActionResult> BookSlot(Guid slotId)
    {
        await _bookTimeSlot.Execute(slotId);
        return Ok();
    }
}