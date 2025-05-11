using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories.Produto;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    
    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Model.Produto> GetAll()
    {
        return _context.Produtos.ToList();
    }
    
    public Model.Produto GetProduto(int id)
    {
        return _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
    }
    
    public Model.Produto CreateProduto(Model.Produto produto)
    {
        if (produto is null)
            throw new ArgumentNullException(nameof(produto));

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return produto;
    }
    
    public Model.Produto UpdateProduto(Model.Produto produto)
    {
        if (produto is null)
            throw new ArgumentNullException(nameof(produto));

        _context.Produtos.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();
        return produto;
    }
    
    public Model.Produto DeleteProduto(int id)
    {
        var produto = _context.Produtos.Find(id);
        
        if (produto is null)
            throw new ArgumentNullException(nameof(produto));

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return produto;
    }
}