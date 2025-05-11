using ApiCatalogo.Context;
using ApiCatalogo.Model;
using ApiCatalogo.Repositories.Produto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController: ControllerBase {

    private readonly IProdutoRepository _produtoRepository;

    public ProdutosController(IProdutoRepository produtoRepository) {
        _produtoRepository = produtoRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {

        return Ok(_produtoRepository.GetAll());
    }

   
    [HttpGet("{id:int}", Name = "ObterProduto")]
    public  ActionResult<Produto> Get(int id)
    {
        return Ok(_produtoRepository.GetProduto(id));
    }

    [HttpPost]
    public ActionResult<Produto> Post(Produto produto) {
        
        if (produto is null) {
            return BadRequest("Por favor insira os dados corretamente do produto");
        }

        var produtoCriado = _produtoRepository.CreateProduto(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new {id = produtoCriado.ProdutoId }, produtoCriado);

    }

    [HttpPut("{id:int}")]
    public ActionResult<Produto> Put(Produto produto, int id) {
        if (id != produto.ProdutoId) {
            return BadRequest("Os ids não são iguais");
        }

        return Ok(_produtoRepository.UpdateProduto(produto));
    }


    [HttpDelete("{id:int}")]
    public ActionResult<Produto> Delete(int id )
    {
        var produto = _produtoRepository.GetProduto(id);

        if (produto is null) {
            return NotFound("Produto Não Localizado!");
        }

        return Ok(_produtoRepository.DeleteProduto(id));
    }

}
