using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Repository;

namespace ApiCatalogo.Repositories.Produto;

public interface IProdutoRepository: IRepository<Model.Produto>
{
    Task<PagedList<Model.Produto>> GetProdutosFilterAsync(FilterProdutos filter);
    Task<PagedList<Model.Produto>> GetPagedListAsync(ProdutosParameters parameters);
    Task<IEnumerable<Model.Produto>> GetProdutosPorCategoriaAsync(int id);
}