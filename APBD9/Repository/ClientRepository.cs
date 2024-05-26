using Microsoft.EntityFrameworkCore;

namespace APBD9.Repository;

public class ClientRepository : IClientRepository
{
    private readonly MasterContext _context;

    public ClientRepository(MasterContext masterContext)
    {
        _context = masterContext;
    }
    
    public async Task DeleteClient(int clientId)
    {
        var client = new Client()
        {
            IdClient = clientId
        };
        _context.Add(client);
        _context.Entry(client).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task<Client?> FindByPeselNumber(string pesel)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(c => c.Pesel == pesel);
    }

    public async Task<int> CreateClient(Client client)
    {
        _context.Add(client);
        await _context.SaveChangesAsync();
       return client.IdClient;
    }
}