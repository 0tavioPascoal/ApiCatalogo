using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories.Categoria;

public class CategoiaRepository: ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoiaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Model.Categoria> GetCategorias()
    {
      return  _context.Categorias.ToList();
    }

    public Model.Categoria GetCategoria(int id)
    {
       return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
       
    }

    public Model.Categoria CreateCategoria(Model.Categoria categoria)
    {
      if(categoria is null)
          throw new ArgumentNullException(nameof(categoria));

      _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public Model.Categoria UpdateCategoria(Model.Categoria categoria)
    {
        if(categoria is null)
            throw new ArgumentNullException(nameof(categoria));
        
        _context.Categorias.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return categoria;
    }
    

    public Model.Categoria DeleteCategoria(int id)
    {
      var category =   _context.Categorias.Find(id);
      
      if(category is null)
          throw new ArgumentNullException(nameof(category));

      _context.Categorias.Remove(category);
        _context.SaveChanges();

        return category;
    }
}