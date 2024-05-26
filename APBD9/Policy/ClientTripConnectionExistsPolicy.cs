using APBD9.Repository;

namespace APBD9.Policy;

public class ClientTripConnectionExistsPolicy : IClientTripConnectionExistsPolicy
{
    private readonly IClientTripsRepository _repository;

    public ClientTripConnectionExistsPolicy(IClientTripsRepository clientTripsRepository)
    {
        _repository = clientTripsRepository;
    }
    public async Task<bool> IsClientConnectedWithAnyTrips(int clientId)
    {
        return await _repository.GetTripsCountByClientId(clientId) > 0;
    }
}