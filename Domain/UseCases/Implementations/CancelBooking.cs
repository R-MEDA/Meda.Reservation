namespace Domain.UseCases.Implementations;

using Domain.Reservation;
using Domain.Ports;
using Domain.Exceptions;
using Domain.TimeSlot;

public class CancelBooking : ICancelBooking
{
    private readonly IReservations _reservations;

    public CancelBooking(IReservations reservationRepository)
    {
        _reservations = reservationRepository;
    }

    public async Task Execute(Guid reservationId)
    {
        Reservation reservation = await _reservations.GetById(reservationId) ?? throw new ReservationNotFoundException(reservationId);
        reservation.Cancel();
    }
}
