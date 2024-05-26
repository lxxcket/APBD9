namespace APBD9.Policy;

public interface IPagingPolicy
{
    bool IsPageNumValid(int pageNum);

    bool IsPageSizeValid(int pageSize);

    bool IsPageNumInValidRange(int pageNum, int totalPages);
}