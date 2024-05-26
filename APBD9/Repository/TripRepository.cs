using Microsoft.EntityFrameworkCore;

namespace APBD9.Repository;

public class TripRepository : ITripRepository
{
    private readonly MasterContext _context;

    public TripRepository(MasterContext context)
    {
        _context = context;
    }

public async Task<List<Trip>> GetTrips(int pageNum, int pageSize)
{

    return await _context.Trips
        .Include(t => t.IdCountries)
        .Include(t => t.ClientTrips)
        .ThenInclude(t => t.IdClientNavigation)
        .OrderBy(d => d.DateFrom)
        .Skip((pageNum - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
}

public async Task<int> TripsCount()
{
    return await _context.Trips.CountAsync();
}

public async Task<Trip?> TripWithClientAssigned(int tripId, string pesel)
{
    Trip? trip = await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == tripId);
    if (trip == null)
    {
        return null;
    }

    ClientTrip? clientTrip =
        await _context.ClientTrips.FirstOrDefaultAsync(t => t.IdTrip == tripId && t.IdClientNavigation.Pesel == pesel);
    return clientTrip != null ? trip : null;
}

public async Task<Trip?> GetTripById(int tripId)
{
    return await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == tripId);
}
}