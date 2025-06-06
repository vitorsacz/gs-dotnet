using System.ComponentModel.DataAnnotations;

namespace AlertaCidadao.Contracts.Dtos.Requests;

public class SafeResourceRequestDto
{
    [Required(ErrorMessage = "O código do recurso é obrigatório.")]
    [StringLength(20, ErrorMessage = "O código do recurso deve ter no máximo 20 caracteres.")]
    public string ResourceCode { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "A latitude é obrigatória.")]
    public decimal Latitude { get; set; }

    [Required(ErrorMessage = "A longitude é obrigatória.")]
    public decimal Longitude { get; set; }

    [Required(ErrorMessage = "A capacidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A capacidade deve ser maior que zero.")]
    public int Capacity { get; set; }
}
