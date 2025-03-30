namespace Domain.UseCases;
public interface ICheckIn
{
    Task Execute(Guid reservationId);
}
