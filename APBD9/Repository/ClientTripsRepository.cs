using Microsoft.EntityFrameworkCore;

namespace APBD9.Repository;

public class ClientTripsRepository : IClientTripsRepository
{
    private readonly MasterContext _context;

    public ClientTripsRepository(MasterContext context)
    {
        _context = context;
    }
    
    public async Task<int> GetTripsCountByClientId(int clientId)
    {
        return await _context.ClientTrips
            .CountAsync(t => t.IdClient == clientId);
    }

    public async Task<int> CreateClientTrip(ClientTrip clientTrip)
    {
        _context.Add(clientTrip);
        return await _context.SaveChangesAsync();
    }
}