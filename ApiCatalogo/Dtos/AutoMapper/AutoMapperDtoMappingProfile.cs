using ApiCatalogo.Dtos.Categoria;
using ApiCatalogo.Dtos.Produtos;
using ApiCatalogo.Model;
using AutoMapper;

namespace ApiCatalogo.Dtos.AutoMapper;

public class AutoMapperDtoMappingProfile : Profile
{
    public AutoMapperDtoMappingProfile()
    {
        CreateMap<Produto, ProdutoDto>().ReverseMap();
        CreateMap<Model.Categoria, CategoriaDto>().ReverseMap();
    }
}