using APBD9.DTOs;
using APBD9.Policy;
using APBD9.Repository;

namespace APBD9.UseCase;

public class CreateClientAndAssignToTheTripUseCase : ICreateClientAndAssignToTheTripUseCase
{
    private readonly ICreateClientValidPolicy _createClientValidPolicy;
    private readonly IClientTripsRepository _clientTripsRepository;
    private readonly IClientRepository _clientRepository;

    public CreateClientAndAssignToTheTripUseCase(ICreateClientValidPolicy createClientValidPolicy,
        IClientTripsRepository clientTripsRepository, IClientRepository clientRepository)
    {
        _createClientValidPolicy = createClientValidPolicy;
        _clientTripsRepository = clientTripsRepository;
        _clientRepository = clientRepository;
    }
    
    public async Task<(bool success, string message)> ExecuteClientCreationAndAssignToTheTrip(ClientTripPostDTO clientTripPostDto)
    {
        if (await _createClientValidPolicy.ClientWithPeselExists(clientTripPostDto.Pesel))
            return (false, "Client with provided PESEL already exists");
        if (await _createClientValidPolicy.ClientWithPeselAssignedToTheTrip(clientTripPostDto.IdTrip,
                clientTripPostDto.Pesel))
            return (false, "Client with provided PESEL is already assigned to the provided trip");
        if (!await _createClientValidPolicy.TripDateFromIsInFuture(clientTripPostDto.IdTrip))
            return (false, "Start of the trip must be in the future");
        
        int clientId = await _clientRepository.CreateClient(new Client()
        {
            FirstName = clientTripPostDto.FirstName,
            LastName = clientTripPostDto.LastName,
            Email = clientTripPostDto.Email,
            Telephone = clientTripPostDto.Telephone,
            Pesel = clientTripPostDto.Pesel
        });
        await _clientTripsRepository.CreateClientTrip(new ClientTrip()
        {
            IdClient = clientId,
            IdTrip = clientTripPostDto.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = clientTripPostDto.PaymentDate
        });
        return (true, "Created successfully");
    }
}