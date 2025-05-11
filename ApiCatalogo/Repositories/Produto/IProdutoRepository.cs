using ApiCatalogo.Repositories.Repository;

namespace ApiCatalogo.Repositories.Produto;

public interface IProdutoRepository: IRepository<Model.Produto>
{
    IEnumerable<Model.Produto> GetProdutosPorCategoria(int id);
}