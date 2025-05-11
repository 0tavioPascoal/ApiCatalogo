using ApiCatalogo.Context;
using ApiCatalogo.Model;
using ApiCatalogo.Repositories.Produto;
using ApiCatalogo.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController: ControllerBase {

  
    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController( IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {

        return Ok(_produtoRepository.GetAll());
    }
    
    [HttpGet("produtos/{id}")]
    public  ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
    {
        var produtos =  Ok(_produtoRepository.GetProdutosPorCategoria(id));
        if(produtos is null)
            return NotFound();
        
        return Ok(produtos);
    }
    
    [HttpGet("{id:int}", Name = "ObterProduto")]
    public  ActionResult<Produto> Get(int id)
    {
        return Ok(_produtoRepository.Get(p => p.ProdutoId == id));
    }

    [HttpPost]
    public ActionResult<Produto> Post(Produto produto) {
        
        if (produto is null) {
            return BadRequest("Por favor insira os dados corretamente do produto");
        }

        var produtoCriado = _produtoRepository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new {id = produtoCriado.ProdutoId }, produtoCriado);

    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(Produto produto, int id) {
        if (id != produto.ProdutoId) {
            return BadRequest("Os ids não são iguais");
        }

        return Ok(_produtoRepository.Update(produto));
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Produto> Delete(int id )
    {
        var produto = _produtoRepository.Get(p => p.ProdutoId == id);

        if (produto is null) {
            return NotFound("Produto Não Localizado!");
        }

        return Ok(_produtoRepository.Delete(produto));
    }

}
