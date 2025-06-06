using Microsoft.AspNetCore.Mvc;
using AlertaCidadao.Application.Services.Interfaces;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace AlertaCidadao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClimaticEventController : ControllerBase
{
    private readonly IClimaticEventService _eventService;

    public ClimaticEventController(IClimaticEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Adiciona um novo evento climático.")]
    [SwaggerResponse(201, "Evento climático criado com sucesso.", typeof(ClimaticEventResponseDto))]
    [SwaggerResponse(400, "Requisição inválida. Verifique os dados fornecidos.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<ClimaticEventResponseDto>> AddEvent([FromBody] ClimaticEventRequestDto request)
    {
        try
        {
            var result = await _eventService.AddEventAsync(request);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obtém todos os eventos climáticos.")]
    [SwaggerResponse(200, "Lista de eventos climáticos obtida com sucesso.", typeof(IEnumerable<ClimaticEventResponseDto>))]
    [SwaggerResponse(404, "Nenhum evento encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<IEnumerable<ClimaticEventResponseDto>>> GetAllEvents()
    {
        try
        {
            var events = await _eventService.GetAllEventsAsync();
            if (events == null || !events.Any())
                return NotFound("Nenhum evento encontrado.");
            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet("{eventCode}")]
    [SwaggerOperation(Summary = "Obtém um evento climático pelo código do evento.")]
    [SwaggerResponse(200, "Evento climático obtido com sucesso.", typeof(ClimaticEventResponseDto))]
    [SwaggerResponse(404, "Evento não encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<ClimaticEventResponseDto>> GetEventByEventCode(string eventCode)
    {
        try
        {
            var evt = await _eventService.GetEventByEventCodeAsync(eventCode);
            if (evt == null)
                return NotFound("Evento não encontrado.");
            return Ok(evt);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut("{eventCode}")]
    [SwaggerOperation(Summary = "Atualiza um evento climático existente pelo código do evento.")]
    [SwaggerResponse(200, "Evento climático atualizado com sucesso.", typeof(ClimaticEventResponseDto))]
    [SwaggerResponse(404, "Evento não encontrado.")]
    [SwaggerResponse(400, "Requisição inválida. Verifique os dados fornecidos.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<ClimaticEventResponseDto>> UpdateEvent(string eventCode, [FromBody] ClimaticEventRequestDto request)
    {
        try
        {
            var result = await _eventService.UpdateEventByEventCodeAsync(eventCode, request);
            if (result == null)
                return NotFound("Evento não encontrado.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpDelete("{eventCode}")]
    [SwaggerOperation(Summary = "Remove um evento climático pelo código do evento.")]
    [SwaggerResponse(200, "Evento deletado com sucesso.")]
    [SwaggerResponse(404, "Evento não encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult> DeleteEvent(string eventCode)
    {
        try
        {
            var deleted = await _eventService.DeleteEventByEventCodeAsync(eventCode);
            if (!deleted)
                return NotFound("Evento não encontrado.");
            return Ok("Evento deletado com sucesso!");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
