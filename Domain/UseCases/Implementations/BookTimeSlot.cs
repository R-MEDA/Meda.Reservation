namespace Domain.UseCases.Implementations;

using Domain.TimeSlot;
using Domain.Reservation;
using Domain.Ports;
using Domain.Exceptions;

public class BookTimeSlot : IBookTimeSlot
{
    private readonly ITimeSlots _timeSlotRepository;
    private readonly IReservations _reservationRepository;

    public BookTimeSlot(ITimeSlots timeSlotRepository, IReservations reservationRepository)
    {
        _timeSlotRepository = timeSlotRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<Reservation> Execute(Guid timeSlotId)
    {
        TimeSlot timeSlot = await _timeSlotRepository.GetById(timeSlotId);

        if (timeSlot == null || timeSlot.IsFullyBooked())
        {
            throw new TimeSlotFullyBookedException(timeSlot.TimeSlotId);
        }

        Reservation reservation = new Reservation(timeSlot);

        await _reservationRepository.Save(reservation);
        timeSlot.AddReservation(reservation);

        return reservation;
    }
}