namespace Domain.UseCases.Implementations;

using Domain.Reservation;
using Domain.Ports;
using Domain.Exceptions;
using Domain.TimeSlot;

public class CancelBooking : ICancelBooking
{
    private readonly IReservations _reservationRepository;
    private readonly ITimeSlots _timeslotRepository;

    public CancelBooking(
        IReservations reservationRepository,
        ITimeSlots timeslotRepository)
    {
        _reservationRepository = reservationRepository;
        _timeslotRepository = timeslotRepository;
    }

    public async Task Execute(Guid reservationId)
    {
        Reservation reservation = await _reservationRepository.GetById(reservationId) ?? throw new ReservationNotFoundException(reservationId);
        // TimeSlot timeslot = await _timeslotRepository.GetById(reservation.TimeSlot.TimeSlotId) ?? throw new TimeSlotNotFoundException(reservation.TimeSlot.TimeSlotId);
        // await _timeslotRepository.Save(timeslot);


        reservation.Cancel();
        await _reservationRepository.Save(reservation);
    }
}
