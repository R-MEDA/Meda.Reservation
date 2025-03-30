namespace Domain.TimeSlot;

using Domain.Reservation;

public class TimeSlot
{
    public static readonly int AvailableSeats = 50;
    public Guid TimeSlotId { get; private set; }
    public DateTime Start { get; private set; }
    public List<Reservation> _reservations { get; private set; }
    public List<Reservation> _checkedInReservations {get; private set; }

    public TimeSlot(DateTime start)
    {
        TimeSlotId = Guid.CreateVersion7();
        Start = start;
        _reservations = [];
        _checkedInReservations = [];
    }
    public bool IsFullyBooked() => _reservations.Count >= AvailableSeats;

    public void AddReservation(Reservation reservation)
    {    
        _reservations.Add(reservation);
    }

    public void MarkReservationAsCheckedIn(Reservation reservation)
    {
        if (_reservations?.Contains(reservation) == true)
        {
            _checkedInReservations.Add(reservation);
        }
    }
}
