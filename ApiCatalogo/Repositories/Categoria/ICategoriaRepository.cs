using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Repository;

namespace ApiCatalogo.Repositories.Categoria;

public interface ICategoriaRepository : IRepository<Model.Categoria>
{
    
    Task<PagedList<Model.Categoria>> GetCategoriaFilterAsync(FilterCategoria filter);
    Task<PagedList<Model.Categoria>> GetPagedListCategoriasAsync(CategoriaParameters parameters);
}