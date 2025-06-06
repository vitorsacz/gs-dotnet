namespace AlertaCidadao.Contracts.Dtos.Responses;

public class UserResponseDto
{
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime RegisterDate { get; set; }
}
