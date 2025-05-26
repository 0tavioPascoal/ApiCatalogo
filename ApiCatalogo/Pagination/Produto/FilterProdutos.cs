namespace ApiCatalogo.Pagination;

public class FilterProdutos : PaginationParameters
{
    public decimal? Preco { get; set; }

    public string? PrecoCriterio { get; set; }
}