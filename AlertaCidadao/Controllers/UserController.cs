using Microsoft.AspNetCore.Mvc;
using AlertaCidadao.Application.Services.Interfaces;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace AlertaCidadao.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Adiciona um novo usuário.")]
    [SwaggerResponse(200, "Usuário adicionado com sucesso.")]
    [SwaggerResponse(400, "Requisição inválida. Verifique os dados fornecidos.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<UserResponseDto>> AddUser([FromBody] UserRequestDto request)
    {
        try
        {
            var result = await _userService.AddUserAsync(request);
            return StatusCode((int)HttpStatusCode.OK, "Usuário adicionado com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Obtém todos os usuários.")]
    [SwaggerResponse(200, "Lista de usuários obtida com sucesso.", typeof(IEnumerable<UserResponseDto>))]
    [SwaggerResponse(404, "Nenhum usuário encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();

            if (users == null || !users.Any())
                return StatusCode((int)HttpStatusCode.NotFound, "Nenhum usuário encontrado.");

            return StatusCode((int)HttpStatusCode.OK, users);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet("{cpf}")]
    [SwaggerOperation(Summary = "Obtém um usuário pelo CPF.")]
    [SwaggerResponse(200, "Usuário obtido com sucesso.", typeof(UserResponseDto))]
    [SwaggerResponse(404, "Nenhum usuário encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<UserResponseDto>> GetUserByCpf(string cpf)
    {
        try
        {
            var user = await _userService.GetUserByCpfAsync(cpf);

            if (user == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Nenhum usuário encontrado.");

            return StatusCode((int)HttpStatusCode.OK, user);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut("{cpf}")]
    [SwaggerOperation(Summary = "Atualiza um usuário existente pelo CPF.")]
    [SwaggerResponse(200, "Usuário atualizado com sucesso!")]
    [SwaggerResponse(404, "Nenhum usuário encontrado.")]
    [SwaggerResponse(400, "Requisição inválida. Verifique os dados fornecidos.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult<UserResponseDto>> UpdateUser(string cpf, [FromBody] UserRequestDto request)
    {
        try
        {
            var result = await _userService.UpdateUserByCpfAsync(cpf, request);

            if (result == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Nenhum usuário encontrado.");

            return StatusCode((int)HttpStatusCode.OK, "Usuário atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpDelete("{cpf}")]
    [SwaggerOperation(Summary = "Remove um usuário pelo CPF.")]
    [SwaggerResponse(200, "Usuário deletado com sucesso!")]
    [SwaggerResponse(404, "Usuário não encontrado.")]
    [SwaggerResponse(500, "Erro interno do servidor.")]
    public async Task<ActionResult> DeleteUser(string cpf)
    {
        try
        {
            var deleted = await _userService.DeleteUserByCpfAsync(cpf);

            if (!deleted)
                return StatusCode((int)HttpStatusCode.NotFound, "Usuário não encontrado.");

            return StatusCode((int)HttpStatusCode.OK, "Usuário deletado com sucesso!");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro interno do servidor: {ex.Message}");
        }
    }
}
