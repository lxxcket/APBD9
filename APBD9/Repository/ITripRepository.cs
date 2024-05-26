namespace APBD9.Repository;

public interface ITripRepository
{
    public Task<List<Trip>> GetTrips(int pageNum, int pageSize);

    public Task<int> TripsCount();

    public Task<Trip?> TripWithClientAssigned(int tripId, string pesel);

    public Task<Trip?> GetTripById(int tripId);
}