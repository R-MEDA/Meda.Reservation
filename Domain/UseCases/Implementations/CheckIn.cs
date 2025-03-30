namespace Domain.UseCases.Implementations;

using Domain.Exceptions;
using Domain.Ports;
using Domain.Reservation;

public class CheckIn : ICheckIn
{
    private readonly IReservations _reservations;

    public CheckIn(IReservations reservations)
    {
        _reservations = reservations;
    }

    public async Task Execute(Guid reservationId)
    {
        Reservation reservation = await _reservations.GetById(reservationId) ?? throw new ReservationNotFoundException(reservationId);

        reservation.CheckIn();

        await _reservations.Save(reservation);
    }
}