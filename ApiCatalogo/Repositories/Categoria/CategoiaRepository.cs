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

    public PagedList<Model.Categoria> GetPagedListCategorias(CategoriaParameters parameters)
    {
        var categorias = GetAll()
            .OrderBy(c => c.CategoriaId)
            .AsQueryable();
        
        return PagedList<Model.Categoria>.ToPagedList(categorias, parameters.PageNumber, parameters.PageSize);
    }
}