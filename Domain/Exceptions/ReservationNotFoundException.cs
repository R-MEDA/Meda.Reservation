using Domain.Reservation;

namespace Domain.Exceptions;

public class ReservationNotFoundException : Exception
{
    public ReservationNotFoundException(Guid reservationId) 
        : base($"Reservation with id {reservationId} was not found.")
    {
    }
}
