using ApiCatalogo.Context;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories.Produto;

public class ProdutoRepository :  Repository<Model.Produto>, IProdutoRepository
{
    private IProdutoRepository _produtoRepositoryImplementation;

    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Model.Produto> GetPagedList(ProdutosParameters parameters)
    {
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
        return PagedList<Model.Produto>.ToPagedList(produtos, parameters.PageNumber, parameters.PageSize);
    }

    public IEnumerable<Model.Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        return GetAll()
            .OrderBy(p => p.Nome)
            .Skip(produtosParameters.PageNumber - 1 * produtosParameters.PageSize)
            .Take(produtosParameters.PageSize)
            .ToList();
    }

    public IEnumerable<Model.Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaID ==id );
    }
}