namespace Domain.Reservation;

using Domain.TimeSlot;

public class Reservation
{
    public static readonly Guid MockCustomer = Guid.CreateVersion7();
    public Guid ReservationId { get; private set; }
    public TimeSlot TimeSlot { get; private set; }
    public Guid CustomerId { get; private set; }
    public ReservationStatus Status { get; private set; }
    public DateTime ReservedAt { get; private set; }

    public Reservation(TimeSlot timeSlot)
    {
        ReservationId = Guid.CreateVersion7();
        CustomerId = MockCustomer;
        TimeSlot = timeSlot;
        Status = ReservationStatus.Confirmed;
        ReservedAt = DateTime.UtcNow;
    }

    public bool CanCancel()
    {
        DateTime currentTime = DateTime.UtcNow;
        return (TimeSlot.Start.Date - currentTime.Date).TotalDays > 1;   
    }

    public void Cancel()
    {
        if (!CanCancel())
        {
            throw new InvalidOperationException("Cannot cancel the reservation within 24 hours of the reserved date.");
        }

        Status = ReservationStatus.Cancelled;
    }

    public void CheckIn()
    {
        Status = ReservationStatus.CheckedIn;
    }
}