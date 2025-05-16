namespace ApiCatalogo.Pagination;

public abstract class PaginationParameters
{
    const int PageSizeMax = 49;
    public int PageNumber { get; set; } = 0;
    private int _pageSize = PageSizeMax; 
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > PageSizeMax) ? PageSizeMax : value;
    } 
}
