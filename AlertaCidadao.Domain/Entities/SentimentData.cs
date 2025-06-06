using Microsoft.ML.Data;

namespace AlertaCidadao.Domain.Entities;

public class SentimentData
{
    [LoadColumn(0)]
    public string? Text { get; set; }

    [LoadColumn(1)]
    public string? Label { get; set; }
}
