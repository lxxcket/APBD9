namespace APBD9.UseCase;

public interface IDeleteClientUseCase
{
    Task<(bool status, string message)> ExecuteClientDeletion(int clientId);
}