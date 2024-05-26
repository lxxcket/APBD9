namespace APBD9.UseCase;

public interface IGetTripsUseCase
{
    public Task<TripsPage> GetTripsPage(int pageNum, int pageSize);
}