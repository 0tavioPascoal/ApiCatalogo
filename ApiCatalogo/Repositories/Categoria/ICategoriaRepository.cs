namespace ApiCatalogo.Repositories.Categoria;

public interface ICategoriaRepository
{
   IEnumerable<Model.Categoria> GetCategorias(); 
   Model.Categoria GetCategoria(int id);
   Model.Categoria CreateCategoria(Model.Categoria categoria); 
   Model.Categoria UpdateCategoria(Model.Categoria categoria);
   Model.Categoria DeleteCategoria(int id);
}