using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Repository;

namespace ApiCatalogo.Repositories.Categoria;

public interface ICategoriaRepository : IRepository<Model.Categoria>
{
    PagedList<Model.Categoria> GetPagedListCategorias(CategoriaParameters parameters);
}