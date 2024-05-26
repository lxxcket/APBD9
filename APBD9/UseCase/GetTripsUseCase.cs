using APBD9.DTOs;
using APBD9.Policy;
using APBD9.Repository;

namespace APBD9.UseCase;

public class GetTripsUseCase : IGetTripsUseCase
{

    private readonly IPagingPolicy _pagingPolicy;
    private readonly ITripRepository _repository;

    public GetTripsUseCase(IPagingPolicy pagingPolicy, ITripRepository repository)
    {
        _pagingPolicy = pagingPolicy;
        _repository = repository;
    }
    public async Task<TripsPage> GetTripsPage(int pageNum, int pageSize)
    {
        if (!_pagingPolicy.IsPageNumValid(pageNum))
            pageNum = 1;
        if (!_pagingPolicy.IsPageSizeValid(pageSize))
            pageSize = 10;

        int totalTrips = await _repository.TripsCount();

        int totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

        if (!_pagingPolicy.IsPageNumInValidRange(pageNum, totalPages))
            pageNum = totalPages;
        var trips = await _repository.GetTrips(pageNum, pageSize);
        var tripsPage = new TripsPage()
        {
            pageNum = pageNum,
            pageSize = pageSize,
            allPages = totalPages,
            trips = trips.Select(t => new TripDTO()
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDTO()
                {
                    Name = c.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(c => new ClientDTO()
                {
                    FirstName = c.IdClientNavigation.FirstName,
                    LastName = c.IdClientNavigation.LastName
                }).ToList()
            }).ToList()
        };
        return tripsPage;
    }
}