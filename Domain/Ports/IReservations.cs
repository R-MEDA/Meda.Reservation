namespace Domain.Ports;

using System.Collections.Generic;
using Domain.Reservation;
public interface IReservations
{
    Task<List<Reservation>> GetAll();
    Task<Reservation?> GetById(Guid id);
    Task Save(Reservation reservation);
}
