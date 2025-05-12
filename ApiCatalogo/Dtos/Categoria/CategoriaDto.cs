using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Dtos.Categoria;

public class CategoriaDto
{
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public  string? Nome { get; set; }

    [Required]
    [StringLength(80)]
    public string? ImagemUrl { get; set; } 
}