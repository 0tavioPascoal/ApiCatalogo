using ApiCatalogo.Repositories.Categoria;
using ApiCatalogo.Repositories.Produto;

namespace ApiCatalogo.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    void Commit();
}