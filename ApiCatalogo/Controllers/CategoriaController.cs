using ApiCatalogo.Context;
using ApiCatalogo.Dtos.Categoria;
using ApiCatalogo.Dtos.Mappings;
using ApiCatalogo.Model;
using ApiCatalogo.Repositories.Categoria;
using ApiCatalogo.Repositories.Repository;
using ApiCatalogo.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[Controller]")]
    public class CategoriaController: ControllerBase {
        

    private readonly IUnitOfWork  _unitOfWork;

    public CategoriaController( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetCategorias()
    {
        var categoria = _unitOfWork.CategoriaRepository.GetAll();
        var categoriasDto = categoria.ToCategoriaDtoList();
        return Ok(categoriasDto);
    }
    
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDto> ObterPorId(int id)
    {
        var categoriaId = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);
        if(categoriaId is null)
            return NotFound();

        var categoriaDto = categoriaId.ToCategoriaDto();
        
       return Ok(categoriaDto);
       
    }

    [HttpPost]
    public ActionResult<CategoriaDto> Criar(CategoriaDto categoriaDto)
    {
        if(categoriaDto is null)
            return BadRequest("Por favor insira os dados corretamente da categoria");
        
        var categoria = categoriaDto.ToCategoria();
        
        var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
        _unitOfWork.Commit();
        
        var novaCategoriaDto = categoriaCriada.ToCategoriaDto();
        return new CreatedAtRouteResult("Obtercategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDto> Modificar(CategoriaDto categoriaDto, int id) {
        if (id != categoriaDto.CategoriaId) {
            return BadRequest("Os ids não são iguais");
        }
        
        var categoria= categoriaDto.ToCategoria();

        _unitOfWork.CategoriaRepository.Update(categoria);
        _unitOfWork.Commit();
        
        var categoriaResponse = categoria.ToCategoriaDto();
        return Ok(categoriaResponse);

    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDto> Deletar(int id)
    {
        var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id); 

        if (categoria == null) {
            return NotFound("categoria não Encontrada");
        }

        _unitOfWork.CategoriaRepository.Delete(categoria);
        _unitOfWork.Commit();
        
        var categoriaResponse = categoria.ToCategoriaDto();
        return Ok(categoriaResponse);
    }

}

