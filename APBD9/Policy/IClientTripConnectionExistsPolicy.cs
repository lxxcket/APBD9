namespace APBD9.Policy;

public interface IClientTripConnectionExistsPolicy
{
    Task<bool> IsClientConnectedWithAnyTrips(int clientId);
}