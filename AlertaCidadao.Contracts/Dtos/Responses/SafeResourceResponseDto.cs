namespace AlertaCidadao.Contracts.Dtos.Responses;

public class SafeResourceResponseDto
{
    public string ResourceCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int Capacity { get; set; }
}
