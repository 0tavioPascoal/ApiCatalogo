using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Dtos.Produtos;

public class ProdutoDto
{
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "O nome do produto deve ter no maximo 20 caracteres")]
    public string? Nome { get; set; }

    [Microsoft.Build.Framework.Required]
    [StringLength(300, ErrorMessage = "A descrição do produto deve ter no maximo 300 caracteres")]
    public string? Descricao { get; set; }

    [Microsoft.Build.Framework.Required]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10, ErrorMessage = "A URL da imagem deve ter no maximo 300 caracteres")]
    public string? ImagemUrl { get; set; }


    public int CategoriaID { get; set; }

}