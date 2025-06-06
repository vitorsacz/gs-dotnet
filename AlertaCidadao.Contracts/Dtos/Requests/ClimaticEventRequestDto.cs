using System.ComponentModel.DataAnnotations;

namespace AlertaCidadao.Contracts.Dtos.Requests;

public class ClimaticEventRequestDto
{
    [Required(ErrorMessage = "O código do evento climatico é obrigatório.")]
    [StringLength(20, ErrorMessage = "O código do evento climatico deve ter no máximo 20 caracteres.")]
    public string EventCode { get; set; }

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [StringLength(50, ErrorMessage = "O tipo deve ter no máximo 50 caracteres.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "A data e hora são obrigatórias.")]
    public DateTime EventTime { get; set; }

    [Required(ErrorMessage = "A latitude é obrigatória.")]
    public decimal Latitude { get; set; }

    [Required(ErrorMessage = "A longitude é obrigatória.")]
    public decimal Longitude { get; set; }

    [Required(ErrorMessage = "O nível de risco é obrigatório.")]
    [Range(1, 5, ErrorMessage = "O nível de risco deve estar entre 1 e 5.")]
    public int RiskLevel { get; set; }
}
