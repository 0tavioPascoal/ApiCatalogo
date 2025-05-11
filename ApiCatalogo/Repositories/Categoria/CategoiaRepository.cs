using ApiCatalogo.Context;
using ApiCatalogo.Repositories.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories.Categoria;

public class CategoiaRepository : Repository<Model.Categoria>, ICategoriaRepository
{
    public CategoiaRepository(AppDbContext context) : base(context)
    {
    }
}