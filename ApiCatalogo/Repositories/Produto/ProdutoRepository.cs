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
    public async Task<PagedList<Model.Produto>> GetProdutosFilterAsync(FilterProdutos produtosFiltroParams)
    {
        var produtos = await GetAllAsync();
            
        

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
        {
            if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
            }
        }
        var produtosFiltrados = PagedList<Model.Produto>.ToPagedList(produtos.AsQueryable(), produtosFiltroParams.PageNumber,
            produtosFiltroParams.PageSize);
        return produtosFiltrados;
    }

    public async Task<PagedList<Model.Produto>> GetPagedListAsync(ProdutosParameters parameters)
    {
        var produtos = await GetAllAsync(); 
        var produtosObtidos =   produtos.OrderBy(p => p.ProdutoId).AsQueryable();
        
        return PagedList<Model.Produto>.ToPagedList(produtosObtidos, parameters.PageNumber, parameters.PageSize);
    }
    
    public async Task<IEnumerable<Model.Produto>> GetProdutosPorCategoriaAsync(int id)
    { 
        var p = await GetAllAsync();
        
        return p.Where(c => c.CategoriaID ==id );
    }
}