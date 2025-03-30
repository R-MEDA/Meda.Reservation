using Domain.Reservation;
using Infra.Resources;
using Infra.Resources.Hypermedia;

public class ReservationResource : HalResource
{
    public Guid ReservationId { get; set; }
    public TimeSlotResource TimeSlot { get; set; }
    public string Status { get; set; }
    public DateTime ReservedAt { get; set; }

    public ReservationResource(Reservation reservation, ILinkService linkService) : base(linkService)
    {
        ReservationId = reservation.ReservationId;
        Status = reservation.Status.ToString();
        ReservedAt = reservation.ReservedAt;
        TimeSlot = new TimeSlotResource(reservation.TimeSlot, linkService);

        AddLink("Reservation", new { id = ReservationId }, "self", "GET");

        if (reservation.CanCancel())
        {
            AddLink("CancelReservation", new { id = reservation.ReservationId }, "cancel-reservation", "POST");
        }
    }
}