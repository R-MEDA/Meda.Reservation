namespace Domain.UseCases.Implementations;

using Domain.TimeSlot;
using Domain.Reservation;
using Domain.Ports;
using Domain.Exceptions;

public class BookTimeSlot : IBookTimeSlot
{
    private readonly ITimeSlots _timeSlots;
    private readonly IReservations _reservations;

    public BookTimeSlot(ITimeSlots timeSlots, IReservations reservations)
    {
        _timeSlots = timeSlots;
        _reservations = reservations;
    }

    public async Task<Reservation> Execute(Guid timeSlotId)
    {
        TimeSlot timeSlot = await _timeSlots.GetById(timeSlotId) ?? throw new TimeSlotNotFoundException(timeSlotId);

        if (timeSlot.IsFullyBooked())
        {
            throw new TimeSlotFullyBookedException(timeSlot.TimeSlotId);
        }

        Reservation reservation = new(timeSlot);
        timeSlot.AddReservation(reservation);

        await _reservations.Save(reservation);

        return reservation;
    }
}