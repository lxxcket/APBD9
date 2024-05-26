using APBD9.DTOs;

namespace APBD9;

public class TripsPage
{
    public int pageNum { get; set; }
    public int pageSize { get; set; }
    public int allPages { get; set; }

    public List<TripDTO> trips { get; set; } = new List<TripDTO>();
}