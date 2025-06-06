using AlertaCidadao.Application.Services.Interfaces;
using AlertaCidadao.Domain.Entities;
using Microsoft.ML;
using System.Globalization;
using System.Text;

namespace AlertaCidadao.Application.Services;

public class SentimentAnalysisService : ISentimentAnalysisService
{
    private readonly MLContext _mlContext;
    private ITransformer _model;

    public SentimentAnalysisService()
    {
        _mlContext = new MLContext();
        TrainModel();
    }

    private void TrainModel()
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "training_data.csv");

        var dataView = _mlContext.Data.LoadFromTextFile<SentimentData>(
            filePath,
            hasHeader: true,
            separatorChar: ',');

        var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
            .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
            .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label"))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        _model = pipeline.Fit(dataView);
    }


    public SentimentPrediction Predict(string text)
    {
        var processedText = PreprocessText(text);

        var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);

        var prediction = predictionEngine.Predict(new SentimentData { Text = processedText });

        var maxScoreIndex = Array.IndexOf(prediction.Score, prediction.Score.Max());
        var labels = new[] { "Positivo", "Negativo" };
        prediction.PredictedLabel = labels[maxScoreIndex];

        return prediction;
    }

    private string PreprocessText(string text)
    {
        text = text.ToLower();

        text = new string(text
            .Normalize(NormalizationForm.FormD)
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray());

        text = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

        text = string.Join(" ", text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

        return text;
    }
}
