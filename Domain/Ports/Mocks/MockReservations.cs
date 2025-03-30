namespace Domain.Ports.Mocks;

using Domain.Reservation;

public class MockReservations : IReservations
{
    private readonly List<Reservation> _reservations = [];

    public Task<List<Reservation>> GetAll()
    {
        return Task.Run(() => _reservations);
    }

    public async Task<Reservation?> GetById(Guid id)
    {
        return await Task.Run(() => _reservations.FirstOrDefault(r => r.ReservationId == id));
    }

    public async Task Save(Reservation reservation)
    {
        await Task.Run(() => _reservations.Add(reservation));
    }
}
