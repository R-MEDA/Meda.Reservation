namespace Domain.Ports.Mocks;

using Domain.Reservation;

public class MockReservations : IReservations
{
    private readonly List<Reservation> _reservations = [];

    public Task<List<Reservation>> GetAll()
    {
        return Task.Run(() => _reservations);
    }

    public Task<Reservation?> GetById(Guid id)
    {
        return Task.Run( () => _reservations.Where(r => r.ReservationId == id).FirstOrDefault());
    }

    public Task Save(Reservation reservation)
    {
        return Task.Run(() => _reservations.Add(reservation));    
    }
}
