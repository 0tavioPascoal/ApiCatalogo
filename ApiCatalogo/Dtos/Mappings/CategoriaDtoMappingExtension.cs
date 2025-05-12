using ApiCatalogo.Dtos.Categoria;

namespace ApiCatalogo.Dtos.Mappings;

public static class CategoriaDtoMappingExtension
{
    public static CategoriaDto? ToCategoriaDto(this Model.Categoria categoria)
    {
        if (categoria is null)
            return null;

        return new CategoriaDto()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };
    }

    public static Model.Categoria? ToCategoria(this CategoriaDto categoriaDto)
    {
        if (categoriaDto is null)
            return null;

        return new Model.Categoria()
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImagemUrl = categoriaDto.ImagemUrl
        };
    }

    public static IEnumerable<CategoriaDto> ToCategoriaDtoList(this IEnumerable<Model.Categoria> categorias)
    {
        if (categorias is null || !categorias.Any())
        {
            return new List<CategoriaDto>();
        }
        
        return categorias.Select(c => new CategoriaDto()
        {
            CategoriaId = c.CategoriaId,
            Nome = c.Nome,
            ImagemUrl = c.ImagemUrl
        }).ToList();
    }
}