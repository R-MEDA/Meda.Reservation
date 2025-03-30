namespace Domain.Exceptions;

public class TimeSlotFullyBookedException : Exception
{
    public TimeSlotFullyBookedException(Guid id) 
        : base($"TimeSlot with id {id} is fully booked")
    {
    }
}
