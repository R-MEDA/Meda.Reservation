using Domain.TimeSlot;

namespace Domain.Exceptions;

public class TimeSlotNotFoundException : Exception
{
    public TimeSlotNotFoundException(Guid id) 
        : base($"TimeSlot with id {id} was not found")
    {
    }
}
