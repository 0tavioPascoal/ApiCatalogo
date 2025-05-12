using ApiCatalogo.Context;
using ApiCatalogo.Dtos.Produtos;
using ApiCatalogo.Model;
using ApiCatalogo.Repositories.Produto;
using ApiCatalogo.Repositories.Repository;
using ApiCatalogo.Repositories.UnitOfWork;
using AutoMapper;
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


    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDto>> Get()
    {
        var produtos = _unitOfWork.ProdutoRepository.GetAll();
        //var destino = _mapper.Map<destino>(origem) 
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        
        return Ok(produtosDto);
    }
    
    [HttpGet("produtos/{id}")]
    public  ActionResult<IEnumerable<ProdutoDto>> GetProdutosPorCategoria(int id)
    {
        var produtos =  Ok(_unitOfWork.ProdutoRepository.GetProdutosPorCategoria(id));
        if(produtos is null)
            return NotFound();
        
        return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produtos));
    }
    
    [HttpGet("{id:int}", Name = "ObterProduto")]
    public  ActionResult<ProdutoDto> Get(int id)
    {
        var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);
        
        return Ok(_mapper.Map<ProdutoDto>(produto));
    }

    [HttpPost]
    public ActionResult<ProdutoDto> Post(ProdutoDto produtoDto) {
        
        if (produtoDto is null) {
            return BadRequest("Por favor insira os dados corretamente do produto");
        }
        var produto = _mapper.Map<Produto>(produtoDto);

        _unitOfWork.ProdutoRepository.Create(produto);
        _unitOfWork.Commit();

        var produtoResponse = _mapper.Map<ProdutoDto>(produto); 

        return new CreatedAtRouteResult("ObterProduto",
            new {id = produtoResponse.ProdutoId }, produtoResponse);

    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDto> Put(ProdutoDto produtoDto, int id) {
        if (id != produtoDto.ProdutoId) {
            return BadRequest("Os ids não são iguais");
        }
        
        var produto = _mapper.Map<Produto>(produtoDto);

        _unitOfWork.ProdutoRepository.Update(produto);
        _unitOfWork.Commit();
        
        var produtoResponse = _mapper.Map<ProdutoDto>(produto);
        return Ok(produtoResponse);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDto> Delete(int id )
    {
        var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null) {
            return NotFound("Produto Não Localizado!");
        }
        _unitOfWork.ProdutoRepository.Delete(produto);
        _unitOfWork.Commit();
        
        var produtoResponse = _mapper.Map<ProdutoDto>(produto);

        return Ok(produtoResponse);
    }

}
