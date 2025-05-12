using ApiCatalogo.Context;
using ApiCatalogo.Repositories.Categoria;
using ApiCatalogo.Repositories.Produto;

namespace ApiCatalogo.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private ICategoriaRepository? _categoriaRepo;

    private IProdutoRepository? _produtoRepo;

    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoiaRepository(_context);
        }
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
        }
        
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}