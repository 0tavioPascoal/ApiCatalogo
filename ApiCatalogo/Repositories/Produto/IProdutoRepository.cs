namespace ApiCatalogo.Repositories.Produto;

public interface IProdutoRepository
{
    IEnumerable<Model.Produto> GetAll();
    Model.Produto GetProduto(int id);
    Model.Produto CreateProduto(Model.Produto produto);
    Model.Produto UpdateProduto(Model.Produto produto);
    Model.Produto DeleteProduto(int id);
    
    
}