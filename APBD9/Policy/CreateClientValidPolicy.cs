using APBD9.Repository;

namespace APBD9.Policy;

public class CreateClientValidPolicy : ICreateClientValidPolicy
{
    private ITripRepository _tripRepository;
    private IClientRepository _clientRepository;

    public CreateClientValidPolicy(ITripRepository tripRepository, IClientRepository clientRepository)
    {
        _tripRepository = tripRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task<bool> ClientWithPeselExists(string pesel)
    {
        var client = await _clientRepository.FindByPeselNumber(pesel);

        return client != null;
    }

    public async Task<bool> ClientWithPeselAssignedToTheTrip(int tripId, string pesel)
    {
        var trip = await _tripRepository.TripWithClientAssigned(tripId, pesel);
        return trip != null;
    }

    public async Task<bool> TripDateFromIsInFuture(int tripId)
    {
        Trip? trip = await _tripRepository.GetTripById(tripId);

        if (trip == null)
            return false;

        return trip.DateFrom > DateTime.Now;
    }
}