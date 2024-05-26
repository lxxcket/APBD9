namespace APBD9.Policy;

public interface ICreateClientValidPolicy
{
    Task<bool> ClientWithPeselExists(string pesel);

    Task<bool> ClientWithPeselAssignedToTheTrip(int tripId, string pesel);

    Task<bool> TripDateFromIsInFuture(int tripId);
    
}