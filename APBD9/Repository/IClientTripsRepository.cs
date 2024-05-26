namespace APBD9.Repository;

public interface IClientTripsRepository
{
    public Task<int> GetTripsCountByClientId(int clientId);

    public Task<int> CreateClientTrip(ClientTrip clientTrip);
}