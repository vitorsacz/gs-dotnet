namespace AlertaCidadao.Contracts.Dtos.Responses;

public class ClimaticEventResponseDto
{
    public string EventCode { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public DateTime EventTime { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int RiskLevel { get; set; }
}
