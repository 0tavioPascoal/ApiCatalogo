using ApiCatalogo.Context;
using ApiCatalogo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[Controller]")]
    public class CategoriaController: ControllerBase {

    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context) {
        _context = context;
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos() {
        var categorias = _context.Categorias.Include(p => p.Produtos).ToList();

        if(categorias == null) {
            return NotFound();
        }

        return Ok(categorias);
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetCategorias() {
        var categorias = _context.Categorias.AsNoTracking().ToList();

        if(categorias == null) {
            return NotFound();
        }

        return Ok(categorias);
    }
    
    

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> ObterPorId(int id) {
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

        if(categoria == null) {
            return NotFound("categoria não Encontrada");
        }

        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult<Categoria> Criar(Categoria categoria) {

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("Obtercategoria", new { id = categoria.CategoriaId }, categoria);

    }

    [HttpPut("{id:int}")]
    public ActionResult<Categoria> Modificar(Categoria categoria, int id) {
        if (id != categoria.CategoriaId) {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<Categoria> Deletar(int id) {
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

        if (categoria == null) {
            return NotFound("categoria não Encontrada");
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return Ok();
    }

}

