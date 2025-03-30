namespace Domain.Ports;

using Domain.TimeSlot;

public interface ITimeSlots
{
    Task<TimeSlot?> GetById(Guid id);
    Task Save(TimeSlot timeSlot);
    Task<List<TimeSlot>> GetAll();
}
