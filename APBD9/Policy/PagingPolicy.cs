namespace APBD9.Policy;

public class PagingPolicy : IPagingPolicy
{
    public bool IsPageNumValid(int pageNum)
    {
        return pageNum > 0;
    }

    public bool IsPageSizeValid(int pageSize)
    {
        return pageSize > 0;
    }

    public bool IsPageNumInValidRange(int pageNum, int totalPages)
    {
        return pageNum < totalPages;
    }
}