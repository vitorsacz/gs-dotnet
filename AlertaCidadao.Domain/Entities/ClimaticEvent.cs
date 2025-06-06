using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlertaCidadao.Domain.Entities;

public class ClimaticEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string EventCode { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public DateTime EventTime { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int RiskLevel { get; set; }
}
