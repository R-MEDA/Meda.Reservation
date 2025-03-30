namespace Domain.UseCases.Implementations;

using Domain.Exceptions;
using Domain.Ports;
using Domain.Reservation;

public class CheckIn : ICheckIn
{
    private readonly IReservations _reservationRepository;

    public CheckIn(IReservations reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task Execute(Guid reservationId)
    {
        Reservation reservation = await _reservationRepository.GetById(reservationId)
            ?? throw new ReservationNotFoundException(reservationId);

        reservation.CheckIn();

        await _reservationRepository.Save(reservation);
    }
}