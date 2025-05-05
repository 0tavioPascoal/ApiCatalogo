using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Model;

[Table("Produtos")]
    public class Produto {

    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "O nome do produto deve ter no maximo 20 caracteres")]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300, ErrorMessage = "A descrição do produto deve ter no maximo 300 caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300, MinimumLength = 10, ErrorMessage = "A URL da imagem deve ter no maximo 300 caracteres")]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaID { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}

