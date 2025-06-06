using AlertaCidadao.Domain.Entities;

namespace AlertaCidadao.Application.Services.Interfaces
{
    public interface ISentimentAnalysisService
    {
        SentimentPrediction Predict(string text);
    }
}