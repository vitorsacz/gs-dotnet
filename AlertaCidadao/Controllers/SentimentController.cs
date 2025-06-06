using AlertaCidadao.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AlertaCidadao.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class SentimentController : ControllerBase
{
    private readonly ISentimentAnalysisService _sentimentService;

    public SentimentController(ISentimentAnalysisService sentimentService)
    {
        _sentimentService = sentimentService;
    }

    [HttpPost("feedback")]
    [SwaggerOperation(Summary = "Analisa o sentimento de um texto de feedback.")]
    [SwaggerResponse(200, "Análise de sentimento realizada com sucesso.")]
    [SwaggerResponse(400, "Requisição inválida. Verifique os dados fornecidos.")]
    [SwaggerResponse(500, "Erro interno no servidor.")]
    public IActionResult AnalyzeSentiment([FromBody] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return BadRequest("Texto não pode ser vazio.");

        var result = _sentimentService.Predict(text);

        string sentiment = result.PredictedLabel == "Positivo" ? "Positivo" : "Negativo";

        return Ok(new
        {
            Text = text,
            Sentiment = sentiment,
            Scores = new
            {
                Positivo = result.Score[0],
                Negativo = result.Score[1]
            }
        });
    }
}
