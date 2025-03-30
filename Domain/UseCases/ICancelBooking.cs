namespace Domain.UseCases;

using Domain.Reservation;

public interface ICancelBooking
{
    Task Execute(Guid reservationId);
}
