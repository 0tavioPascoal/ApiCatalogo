using ApiCatalogo.Context;
using ApiCatalogo.Dtos.Categoria;
using ApiCatalogo.Dtos.Mappings;
using ApiCatalogo.Model;
using ApiCatalogo.Pagination;
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
    
    [HttpGet("paginationCategory")]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> paginationCategory([FromQuery] CategoriaParameters parameters)
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetPagedListCategoriasAsync(parameters);


        return ObterCategoria(categorias);
    }

    [HttpGet("filterCategory")]
   public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObterCategoriasFilter([FromQuery] FilterCategoria filter)
    {
        var categorias = await _unitOfWork.CategoriaRepository.GetCategoriaFilterAsync(filter);
        
        if (categorias is null)
            return NotFound("Nenhuma categoria encontrada");
        
        return ObterCategoria(categorias);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetAllAsync();
        var categoriasDto = categoria.ToCategoriaDtoList();
        return Ok(categoriasDto);
    }
    
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDto>> ObterPorId(int id) 
    {
        
        var categoriaId = await _unitOfWork.CategoriaRepository.GetAsync(c => c.CategoriaId == id);
        if(categoriaId is null)
            return NotFound();

        var categoriaDto = categoriaId.ToCategoriaDto();
        
       return Ok(categoriaDto);
       
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Criar(CategoriaDto categoriaDto)
    {
        if(categoriaDto is null)
            return BadRequest("Por favor insira os dados corretamente da categoria");
        
        var categoria = categoriaDto.ToCategoria();
        
        var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
         await _unitOfWork.CommitAsync();
        
        var novaCategoriaDto = categoriaCriada.ToCategoriaDto();
        return new CreatedAtRouteResult("Obtercategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDto>> Modificar(CategoriaDto categoriaDto, int id) {
        if (id != categoriaDto.CategoriaId) {
            return BadRequest("Os ids não são iguais");
        }
        
        var categoria= categoriaDto.ToCategoria();

        _unitOfWork.CategoriaRepository.Update(categoria);
         await _unitOfWork.CommitAsync();
        
        var categoriaResponse = categoria.ToCategoriaDto();
        return Ok(categoriaResponse);

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDto>> Deletar(int id)
    {
        var categoria = await _unitOfWork.CategoriaRepository.GetAsync(c => c.CategoriaId == id); 

        if (categoria == null) {
            return NotFound("categoria não Encontrada");
        }

        _unitOfWork.CategoriaRepository.Delete(categoria);
         await _unitOfWork.CommitAsync();
        
        var categoriaResponse = categoria.ToCategoriaDto();
        return Ok(categoriaResponse);
    }
    
    private ActionResult<IEnumerable<CategoriaDto>> ObterCategoria(PagedList<Categoria> categorias)
    {
        var metadata = new
        {
            categorias.TotalCount, 
            categorias.PageSize, 
            categorias.CurrentPage, 
            categorias.TotalPages, 
            categorias.HasNext,
            categorias.HasPrevius
        };
        
        Response.Headers.Add("X-Pagination",  System.Text.Json.JsonSerializer.Serialize(metadata));
        
        var categoriasDto = categorias.ToCategoriaDtoList();
        
        return Ok(categoriasDto);
    }

}

