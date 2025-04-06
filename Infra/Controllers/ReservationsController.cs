using Microsoft.AspNetCore.Mvc;
using Domain.UseCases;
using Domain.Reservation;
using Domain.Ports;
using Infra.Resources.Hypermedia;

namespace Infra.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ICancelBooking _cancelBooking;
    private readonly IReservations _reservations;
    private readonly ILinkService _linkService;

    public ReservationsController(
        ICancelBooking cancelBooking,
        IReservations reservations,
        ILinkService linkService
        )
    {
        _cancelBooking = cancelBooking;
        _reservations = reservations;
        _linkService = linkService;
    }

    [HttpGet("{id}",Name = "Reservation")]
    public async Task<ActionResult<ReservationResource[]>> GetById(Guid id)
    {
        List<Reservation> reservations = await _reservations.GetAll();
        Reservation? reservation = reservations.FirstOrDefault(r => r.ReservationId == id);

        if(reservation == null)
        {
            return NotFound(id);
        }

        return Ok(new ReservationResource(reservation, _linkService));
    }

    [HttpGet(Name = "Reservations")]
    public async Task<ActionResult<ReservationResource[]>> GetAll()
    {
        List<Reservation> reservations = await _reservations.GetAll();
        List<ReservationResource> resources = [.. reservations.Select(r => new ReservationResource(r, _linkService))];
        return Ok(resources);
    }

    [HttpDelete("{id}", Name="CancelReservation")]
    public async Task<ActionResult> Cancel(Guid id)
    {
        await _cancelBooking.Execute(id);

        return Ok();
    }
}