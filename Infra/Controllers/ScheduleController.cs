using Microsoft.AspNetCore.Mvc;
using Domain.Ports;
using Infra.Resources;
using Domain.TimeSlot;
using Domain.Exceptions;
using Domain.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infra.Controllers;

[ApiController]
[Route("api/schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ITimeSlots _timeslotRepository;
    private readonly IBookTimeSlot _createBooking;
    private readonly LinkGenerator _linkGenerator;
    
    public ScheduleController(
        ITimeSlots timeslotRepository,
        IBookTimeSlot createBooking,
        LinkGenerator linkGenerator
        )
    {
        _timeslotRepository = timeslotRepository;
        _createBooking = createBooking;
        _linkGenerator = linkGenerator;
    }

    [HttpGet("slots/{id}", Name="GetSlotById")]
    public async Task<ActionResult<TimeSlotResource>> GetById(Guid id)
    {
        TimeSlot timeslot = await _timeslotRepository.GetById(id) ?? throw new TimeSlotNotFoundException(id);
        if (timeslot == null)
        {
            return NotFound();
        }

        return Ok();

        // return Ok(TimeSlotResource.FromTimeSlot(timeslot, Url));
    }


    [HttpGet("available-slots", Name ="AvailableSlots")]
    public async Task<ActionResult<TimeSlotResource>> GetAll()
    {
        var timeslots = await _timeslotRepository.GetAll();
        var resources = timeslots.Select(t => TimeSlotResource.FromTimeSlot(t, HttpContext, _linkGenerator));
        return Ok(resources);       
    }

    [HttpPost("slots/{slotId}/book", Name = "BookSlot")]
    public async Task<ActionResult> BookSlot(Guid slotId)
    {
        try
        {
            var reservationId = await _createBooking.Execute(slotId);

            return Ok();

        }
        catch (TimeSlotNotFoundException ex)
        {
            return Ok(ex.Message);
        }
    }
}



//     [HttpPost("slots")]
//     public async Task<ActionResult<TimeSlotResource>> Create([FromBody] CreateTimeslotRequest request)
//     {
//         var timeslot = new TimeSlot(request.StartTime);
//         await _timeslotRepository.Save(timeslot);
        
//         var resource = TimeSlotResource.FromTimeSlot(timeslot, Url);
//         return CreatedAtAction(nameof(GetById), new { id = timeslot.TimeSlotId }, resource);
//     }


// }

// public class CreateTimeslotRequest
// {
//     public DateTime StartTime { get; set; }
// }

// public class BookSlotRequest 
// {
//     public Guid CustomerId { get; set; }
// }