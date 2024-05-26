using APBD9.DTOs;

namespace APBD9.UseCase;

public interface ICreateClientAndAssignToTheTripUseCase
{
    Task<(bool success, string message)> ExecuteClientCreationAndAssignToTheTrip(ClientTripPostDTO clientTripPostDto);
}