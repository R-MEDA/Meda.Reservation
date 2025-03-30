namespace Domain.UseCases;
public interface ICancelBooking
{
    Task Execute(Guid reservationId);
}
