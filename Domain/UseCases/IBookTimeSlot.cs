namespace Domain.UseCases;

using Domain.Reservation;
using Domain.TimeSlot;

public interface IBookTimeSlot
{
    Task<Reservation> Execute(Guid timeSlotId);
}
