using ApiCatalogo.Context;
using ApiCatalogo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController: ControllerBase {

    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> Get() {
        
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

   
    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<Produto>> Get(int id) {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
        if (produto == null) {
            return NotFound("Produto não Encontrado!");
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult<Produto> Post(Produto produto) {
        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto",
            new {id = produto.ProdutoId }, produto);

    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(Produto produto, int id) {
        if (id != produto.ProdutoId) {
            return BadRequest();
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Produto> Delete(int id ) {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto == null) {
            return NotFound("Produto Não Localizado!");
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok();
    }

}
