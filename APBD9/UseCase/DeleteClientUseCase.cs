using APBD9.Policy;
using APBD9.Repository;

namespace APBD9.UseCase;

public class DeleteClientUseCase : IDeleteClientUseCase
{
    private readonly IClientTripConnectionExistsPolicy _clientTripConnectionExistsPolicy;
    private readonly IClientRepository _clientRepository;

    public DeleteClientUseCase(IClientTripConnectionExistsPolicy policy, IClientRepository repository)
    {
        _clientTripConnectionExistsPolicy = policy;
        _clientRepository = repository;
    }
    
    public async Task<(bool status, string message)> ExecuteClientDeletion(int clientId)
    {
        if (await _clientTripConnectionExistsPolicy.IsClientConnectedWithAnyTrips(clientId))
        {
            return (false, "Cannot delete client because it is connected with one or more trips");
        }

        await _clientRepository.DeleteClient(clientId);
        return (true, "Deleted");
    }
}