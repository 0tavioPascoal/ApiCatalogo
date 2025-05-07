using ApiCatalogo.Context;
using ApiCatalogo.Model;
using ApiCatalogo.Repositories.Categoria;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[Controller]")]
    public class CategoriaController: ControllerBase {
        

    private readonly ICategoriaRepository _repository;

    public CategoriaController(ICategoriaRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetCategorias()
    {
        return Ok(_repository.GetCategorias());
    }
    
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> ObterPorId(int id) 
    {
       return Ok(_repository.GetCategoria(id));
    }

    [HttpPost]
    public ActionResult<Categoria> Criar(Categoria categoria)
    {
        if(categoria is null)
            return BadRequest("Por favor insira os dados corretamente da categoria");
        
        var categoriaCriada = _repository.CreateCategoria(categoria);
        return new CreatedAtRouteResult("Obtercategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Categoria> Modificar(Categoria categoria, int id) {
        if (id != categoria.CategoriaId) {
            return BadRequest("Os ids não são iguais");
        }

        return Ok(_repository.UpdateCategoria(categoria));

    }

    [HttpDelete("{id:int}")]
    public ActionResult<Categoria> Deletar(int id)
    {
        var categoria = _repository.GetCategoria(id); 

        if (categoria == null) {
            return NotFound("categoria não Encontrada");
        }

        return Ok(_repository.DeleteCategoria(id));
    }

}

