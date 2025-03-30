namespace Domain.UseCases;

using Domain.Reservation;
public interface ICheckIn
{
    Task Execute(Guid reservationId);
}
