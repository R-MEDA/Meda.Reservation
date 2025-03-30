namespace Domain.UseCases;

using Domain.Reservation;

public interface IBookTimeSlot
{
    Task<Reservation> Execute(Guid timeSlotId);
}
