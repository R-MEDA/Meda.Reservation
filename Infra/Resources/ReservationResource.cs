using Domain.Reservation;
using Infra.Resources;
using Infra.Resources.Hypermedia;

public class ReservationResource : HalResource
{
    public Guid ReservationId { get; private set; }
    public TimeSlotResource TimeSlot { get; private set; }
    public string Status { get; private set; }
    public DateTime ReservedAt { get; private set; }

    public static ReservationResource FromReservation(Reservation reservation, HttpContext context, LinkGenerator linkGenerator)
    {
        var resource = new ReservationResource
        {
            ReservationId = reservation.ReservationId,
            Status = reservation.Status.ToString(),
            ReservedAt = reservation.ReservedAt,
            TimeSlot = TimeSlotResource.FromTimeSlot(reservation.TimeSlot, context, linkGenerator)
        };

        if (reservation.CanCancel())
        {
            resource.AddLink(
                new Link(
                    linkGenerator.GetUriByName(context, "CancelReservation", new { id = reservation.ReservationId }),
                    "cancel-reservation",
                    "DELETE"
                )
            );
        }

        return resource;
    }
}