using Microsoft.AspNetCore.Mvc;
using AlertaCidadao.Application.Services.Interfaces;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace AlertaCidadao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SafeResourceController : ControllerBase
{
    private readonly ISafeResourceService _resourceService;

    public SafeResourceController(ISafeResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Adiciona um novo recurso seguro.")]
    [SwaggerResponse(201, "Recurso seguro criado com sucesso.", typeof(SafeResourceResponseDto))]
    [SwaggerResponse(400, "Requisição inválida. Verifique os dados fornecidos.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<SafeResourceResponseDto>> AddResource([FromBody] SafeResourceRequestDto request)
    {
        try
        {
            var result = await _resourceService.AddAsync(request);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obtém todos os recursos seguros.")]
    [SwaggerResponse(200, "Lista de recursos seguros obtida com sucesso.", typeof(IEnumerable<SafeResourceResponseDto>))]
    [SwaggerResponse(404, "Nenhum recurso encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<IEnumerable<SafeResourceResponseDto>>> GetAllResources()
    {
        try
        {
            var resources = await _resourceService.GetAllAsync();
            if (resources == null || !resources.Any())
                return NotFound("Nenhum recurso encontrado.");
            return Ok(resources);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet("{resourceCode}")]
    [SwaggerOperation(Summary = "Obtém um recurso seguro pelo código do recurso.")]
    [SwaggerResponse(200, "Recurso seguro obtido com sucesso.", typeof(SafeResourceResponseDto))]
    [SwaggerResponse(404, "Recurso não encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<SafeResourceResponseDto>> GetResourceByCode(string resourceCode)
    {
        try
        {
            var resource = await _resourceService.GetByResourceCodeAsync(resourceCode);
            if (resource == null)
                return NotFound("Recurso não encontrado.");
            return Ok(resource);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut("{resourceCode}")]
    [SwaggerOperation(Summary = "Atualiza um recurso seguro existente pelo código do recurso.")]
    [SwaggerResponse(200, "Recurso seguro atualizado com sucesso.", typeof(SafeResourceResponseDto))]
    [SwaggerResponse(404, "Recurso não encontrado.")]
    [SwaggerResponse(400, "Requisição inválida. Verifique os dados fornecidos.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<SafeResourceResponseDto>> UpdateResource(string resourceCode, [FromBody] SafeResourceRequestDto request)
    {
        try
        {
            var result = await _resourceService.UpdateByResourceCodeAsync(resourceCode, request);
            if (result == null)
                return NotFound("Recurso não encontrado.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpDelete("{resourceCode}")]
    [SwaggerOperation(Summary = "Remove um recurso seguro pelo código do recurso.")]
    [SwaggerResponse(200, "Recurso deletado com sucesso.")]
    [SwaggerResponse(404, "Recurso não encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult> DeleteResource(string resourceCode)
    {
        try
        {
            var deleted = await _resourceService.DeleteByResourceCodeAsync(resourceCode);
            if (!deleted)
                return NotFound("Recurso não encontrado.");
            return Ok("Recurso deletado com sucesso!");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
