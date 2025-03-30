using Domain.TimeSlot;
using Infra.Resources.Hypermedia;

namespace Infra.Resources;

public class TimeSlotResource : HalResource
{
    public TimeSlotResource(ILinkService linkService) : base(linkService) { }

    public Guid Id { get; private set; }
    public DateTime StartTime { get; private set; }
    public int AvailableSeats { get; private set; }
    public bool IsFullyBooked { get; private set; }
    public List<Link> Links { get; private set; }

    public static TimeSlotResource FromTimeSlot(TimeSlot timeslot, ILinkService _linkService, HttpContext context, LinkGenerator linkGenerator)
    {
        var resource = new TimeSlotResource(_linkService)
        {
            Id = timeslot.TimeSlotId,
            StartTime = timeslot.Start,
            AvailableSeats = TimeSlot.AvailableSeats - timeslot._reservations.Count,
            IsFullyBooked = timeslot.IsFullyBooked()
        };

        resource.AddLink("GetSlotById", new { id = resource.Id }, "self", "GET");

        if (!resource.IsFullyBooked)
        {
            resource.AddLink("BookSlot", new { slotId = resource.Id }, "book-slot", "POST");
        }
        return resource;
    }
}