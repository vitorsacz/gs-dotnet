namespace AlertaCidadao.Domain.Entities;

public class SentimentPrediction
{
    public string? PredictedLabel { get; set; }
    public float[]? Score { get; set; }
}
