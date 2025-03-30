namespace Domain.Ports.Mocks;

using Domain.Reservation;
using Domain.TimeSlot;

public class MockTimeSlots : ITimeSlots
{
    private readonly List<TimeSlot> _timeSlots =
    [
        new TimeSlot(DateTime.Today.AddHours(10)),
        new TimeSlot(DateTime.Today.AddHours(11)),
        new TimeSlot(DateTime.Today.AddMonths(3).AddHours(14)),
        new TimeSlot(DateTime.Today.AddHours(15)),
        new TimeSlot(DateTime.Today.AddHours(16))
    ];

    public MockTimeSlots()
    {
        TimeSlot fullyBookedTimeSlot = _timeSlots[4];
        TimeSlot partiallyBookedTimeSlot = _timeSlots[2];

        // Fully booking a timeslot for demonstration purposes
        // Fully booked timeslots won't be provided with a book link in the resource

        for (int i = 0; i < TimeSlot.AvailableSeats; i++)
        {
            fullyBookedTimeSlot.AddReservation(new Reservation(fullyBookedTimeSlot));
        }


        for (int i = 0; i < TimeSlot.AvailableSeats / 2; i++)
        {
            partiallyBookedTimeSlot.AddReservation(new Reservation(partiallyBookedTimeSlot));
        }
    }

    public async Task<TimeSlot?> GetById(Guid id)
    {
        return await Task.Run(() => _timeSlots.Find(ts => ts.TimeSlotId == id));
    }

    public async Task Save(TimeSlot timeSlot)
    {
        await Task.Run(() => _timeSlots.Add(timeSlot));
    }

    public async Task<List<TimeSlot>> GetAll()
    {
        return await Task.Run(() => _timeSlots);
    }
}