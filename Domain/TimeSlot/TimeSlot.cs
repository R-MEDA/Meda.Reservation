namespace Domain.TimeSlot;

using Domain.Reservation;

public class TimeSlot
{
    public static readonly int AvailableSeats = 50;
    public Guid TimeSlotId { get; private set; }
    public DateTime Start { get; private set; }
    public List<Reservation> Reservations { get; private set; }
    public List<Reservation> CheckedInReservations { get; private set; }

    public TimeSlot(DateTime start)
    {
        TimeSlotId = Guid.CreateVersion7();
        Start = start;
        Reservations = [];
        CheckedInReservations = [];
    }
    public bool IsFullyBooked() => Reservations.Count >= AvailableSeats;

    public void AddReservation(Reservation reservation)
    {
        Reservations.Add(reservation);
    }

    public void MarkReservationAsCheckedIn(Reservation reservation)
    {
        if (Reservations?.Contains(reservation) == true)
        {
            CheckedInReservations.Add(reservation);
        }
    }

    public void CancelReservation(Reservation reservation)
    {
        Reservations.Remove(reservation);
    }
}
