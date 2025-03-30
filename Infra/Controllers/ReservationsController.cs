using Microsoft.AspNetCore.Mvc;
using Domain.UseCases;
using Domain.Reservation;
using Domain.Exceptions;
using Domain.Ports;

namespace Infra.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ICancelBooking _cancelBooking;
    private readonly IReservations _reservations;

    public ReservationsController(
        ICancelBooking cancelBooking,
        IReservations reservations)
    {
        _cancelBooking = cancelBooking;
        _reservations = reservations;
    }

    [HttpGet]
    public async Task<ActionResult<ReservationResource[]>> GetAll()
    {
        List<Reservation> reservations = await _reservations.GetAll();
        var resources = reservations.Select(r => ReservationResource.FromReservation(r, Url)).ToArray();
        return Ok(resources);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Cancel(Guid id)
    {
        try
        {
            await _cancelBooking.Execute(id);
            return Ok();
        }
        catch (ReservationNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
