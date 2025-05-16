using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Repository;

namespace ApiCatalogo.Repositories.Produto;

public interface IProdutoRepository: IRepository<Model.Produto>
{
    
    PagedList<Model.Produto> GetPagedList(ProdutosParameters parameters);
    IEnumerable<Model.Produto> GetProdutos(ProdutosParameters produtosParameters);
    IEnumerable<Model.Produto> GetProdutosPorCategoria(int id);
}