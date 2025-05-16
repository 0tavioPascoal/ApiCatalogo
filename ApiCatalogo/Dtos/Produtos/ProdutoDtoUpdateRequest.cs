using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Dtos.Produtos;

public class ProdutoDtoUpdateRequest : IValidatableObject
{
    private IValidatableObject _validatableObjectImplementation;

    [Range(1, 9999, ErrorMessage = "O Estoque deve estar entre 1 e 9999")]
    public float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DataCadastro <= DateTime.Now)
        {
            yield return new ValidationResult("A data de cadastro não pode ser menor que a data atual",
                new[] { nameof(DataCadastro) });
        } 
    }
}