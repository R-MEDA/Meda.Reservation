using Domain.TimeSlot;
using Infra.Resources.Hypermedia;

namespace Infra.Resources;

public class TimeSlotResource : HalResource
{
    public Guid Id { get; private set; }
    public DateTime StartTime { get; private set; }
    public int AvailableSeats { get; private set; }
    public bool IsFullyBooked { get; private set; }

    public TimeSlotResource(TimeSlot timeSlot, ILinkService linkService) : base(linkService)
    {
        Id = timeSlot.TimeSlotId;
        StartTime = timeSlot.Start;
        AvailableSeats = TimeSlot.AvailableSeats - timeSlot._reservations.Count;
        IsFullyBooked = timeSlot.IsFullyBooked();

        AddLink("GetSlotById", new { id = Id }, "self", "GET");

        if (!IsFullyBooked)
        {
            AddLink("BookSlot", new { slotId = Id }, "book-slot", "POST");
        }
    }
}