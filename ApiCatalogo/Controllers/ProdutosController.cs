using ApiCatalogo.Context;
using ApiCatalogo.Dtos.Produtos;
using ApiCatalogo.Model;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories.Produto;
using ApiCatalogo.Repositories.Repository;
using ApiCatalogo.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutosController: ControllerBase {

  
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    [HttpGet("filter/pagenation")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetFilterProdutos ([FromQuery] FilterProdutos filterProdutos)
    {
        var produtos = await _unitOfWork.ProdutoRepository.GetProdutosFilterAsync(filterProdutos);
        return ObterProdutos(produtos);
    }
    
    
    [HttpGet("pagination2")]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutos2([FromQuery] ProdutosParameters produtosParameters)
    {
        var produtos = await _unitOfWork.ProdutoRepository.GetPagedListAsync(produtosParameters);

        return ObterProdutos(produtos);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> Get()
    {
        var produtos = await _unitOfWork.ProdutoRepository.GetAllAsync();
        //var destino = _mapper.Map<destino>(origem) 
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        
        return Ok(produtosDto);
    }
    
    [HttpGet("produtos/{id}")]
    public async  Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutosPorCategoria(int id)
    {
        var produtos =  Ok(await _unitOfWork.ProdutoRepository.GetProdutosPorCategoriaAsync(id));
        if(produtos is null)
            return NotFound();
        
        return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produtos));
    }
    
    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<ProdutoDto>> Get(int id)
    {
        var produto = await _unitOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
        
        return Ok(_mapper.Map<ProdutoDto>(produto));
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Post(ProdutoDto produtoDto) {
        
        if (produtoDto is null) {
            return BadRequest("Por favor insira os dados corretamente do produto");
        }
        var produto = _mapper.Map<Produto>(produtoDto);

        _unitOfWork.ProdutoRepository.Create(produto);
         await _unitOfWork.CommitAsync();

        var produtoResponse = _mapper.Map<ProdutoDto>(produto); 

        return new CreatedAtRouteResult("ObterProduto",
            new {id = produtoResponse.ProdutoId }, produtoResponse);

    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> Put(ProdutoDto produtoDto, int id) {
        if (id != produtoDto.ProdutoId) {
            return BadRequest("Os ids não são iguais");
        }
        
        var produto = _mapper.Map<Produto>(produtoDto);

        _unitOfWork.ProdutoRepository.Update(produto);
        await _unitOfWork.CommitAsync();
        
        var produtoResponse = _mapper.Map<ProdutoDto>(produto);
        return Ok(produtoResponse);
    }

    [HttpPatch("{id:int}/UpdatePartial")]
    public async Task<ActionResult<ProdutoDto>> PartialUpdatePartial(int id, JsonPatchDocument<ProdutoDtoUpdateRequest> patchProdutoDto)
    {
        if (patchProdutoDto is null || id <= 0)
            return BadRequest("Por favor insira os dados corretamente do produto");

        var produto = await _unitOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado!");
        
        var produtoDto = _mapper.Map<ProdutoDtoUpdateRequest>(produto);
        
        patchProdutoDto.ApplyTo(produtoDto, ModelState);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var produtoAtualizado = _mapper.Map<Produto>(produtoDto);
        _unitOfWork.ProdutoRepository.Update(produtoAtualizado);
         await _unitOfWork.CommitAsync();
        var produtoResponse = _mapper.Map<ProdutoDto>(produtoAtualizado);
        return Ok(produtoResponse);
        
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDto>> Delete(int id )
    {
        var produto = await _unitOfWork.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null) {
            return NotFound("Produto Não Localizado!");
        }
        _unitOfWork.ProdutoRepository.Delete(produto);
        await _unitOfWork.CommitAsync();
        
        var produtoResponse = _mapper.Map<ProdutoDto>(produto);

        return Ok(produtoResponse);
    }
    
    private ActionResult<IEnumerable<ProdutoDto>> ObterProdutos(PagedList<Produto> produtos)
    {
        var metadata = new
        {
            produtos.TotalCount, 
            produtos.PageSize, 
            produtos.CurrentPage, 
            produtos.TotalPages, 
            produtos.HasNext,
            produtos.HasPrevius
        };
        
        Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));
        
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        
        return Ok(produtosDto);
    }

}
