namespace APBD9.Repository;

public interface IClientRepository
{
    Task DeleteClient(int clientId);

    Task<Client?> FindByPeselNumber(string pesel);
    Task<int> CreateClient(Client client);
}