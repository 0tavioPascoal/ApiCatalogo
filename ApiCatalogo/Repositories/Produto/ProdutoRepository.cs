using ApiCatalogo.Context;
using ApiCatalogo.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories.Produto;

public class ProdutoRepository :  Repository<Model.Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Model.Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaID == id);
    }
}