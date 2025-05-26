using ApiCatalogo.Context;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories.Categoria;

public class CategoiaRepository : Repository<Model.Categoria>, ICategoriaRepository
{
    public CategoiaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Model.Categoria>> GetCategoriaFilterAsync(FilterCategoria filter)
    {

        var categoria = await GetAllAsync();
        
        if (!string.IsNullOrEmpty(filter.Nome))
        {
            categoria = categoria.Where(c => c.Nome.Contains(filter.Nome));
        } 
        
        var categoriasFiltradas = PagedList<Model.Categoria>.ToPagedList(categoria.AsQueryable(), filter.PageNumber,
            filter.PageSize);

        return categoriasFiltradas;
    }

    public async Task<PagedList<Model.Categoria>> GetPagedListCategoriasAsync(CategoriaParameters parameters)
    {
        var categorias = await GetAllAsync();

        var categoriasObtidas = categorias.OrderBy(p => p.CategoriaId).AsQueryable();
            
        
        return PagedList<Model.Categoria>.ToPagedList(categoriasObtidas, parameters.PageNumber, parameters.PageSize);
    }
}