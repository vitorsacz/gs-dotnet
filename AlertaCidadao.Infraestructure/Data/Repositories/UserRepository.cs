using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using AlertaCidadao.Domain.Entities;
using AlertaCidadao.Infraestructure.Data.AppData;

namespace AlertaCidadao.Infraestructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> AddAsync(UserRequestDto requestDto)
    {
        var user = _mapper.Map<User>(requestDto);

        _context.Add(user);
        await _context.SaveChangesAsync();

        var responseDto = _mapper.Map<UserResponseDto>(user);
        return responseDto;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        var responseDtos = _mapper.Map<IEnumerable<UserResponseDto>>(users);
        return responseDtos;
    }

    public async Task<UserResponseDto?> GetByCpfAsync(string cpf)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);
        if (user == null) return null;

        var responseDto = _mapper.Map<UserResponseDto>(user);
        return responseDto;
    }

    public async Task<UserResponseDto?> UpdateByCpfAsync(string cpf, UserRequestDto requestDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);
        if (user == null) return null;

        user.Name = requestDto.Name;
        user.Cpf = requestDto.Cpf;
        user.Email = requestDto.Email;
        user.Phone = requestDto.Phone;

        await _context.SaveChangesAsync();

        var responseDto = _mapper.Map<UserResponseDto>(user);
        return responseDto;
    }

    public async Task<bool> DeleteByCpfAsync(string cpf)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
