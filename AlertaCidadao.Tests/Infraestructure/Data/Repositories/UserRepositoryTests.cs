using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using AlertaCidadao.Domain.Entities;
using AlertaCidadao.Infraestructure.Data.AppData;
using AlertaCidadao.Infraestructure.Data.Repositories;

namespace AlertaCidadao.Tests.Infraestructure.Data.Repositories;

public class UserRepositoryTests
{
    private readonly ApplicationContext _context;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationContext(options);
        _mapperMock = new Mock<IMapper>();

        _repository = new UserRepository(_context, _mapperMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddUser()
    {
        // Arrange
        var requestDto = new UserRequestDto
        {
            Name = "John Doe",
            Cpf = "12345678900",
            Email = "john@example.com",
            Phone = "123456789"
        };

        var userEntity = new User
        {
            Name = requestDto.Name,
            Cpf = requestDto.Cpf,
            Email = requestDto.Email,
            Phone = requestDto.Phone
        };

        var responseDto = new UserResponseDto
        {
            Name = userEntity.Name,
            Cpf = userEntity.Cpf,
            Email = userEntity.Email,
            Phone = userEntity.Phone
        };

        _mapperMock.Setup(m => m.Map<User>(requestDto)).Returns(userEntity);
        _mapperMock.Setup(m => m.Map<UserResponseDto>(userEntity)).Returns(responseDto);

        // Act
        var result = await _repository.AddAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(requestDto.Cpf, result.Cpf);
        Assert.Equal(requestDto.Name, result.Name);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var userEntity = new User
        {
            Name = "Jane Doe",
            Cpf = "98765432100",
            Email = "jane@example.com",
            Phone = "987654321"
        };

        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync();

        var responseDtos = new List<UserResponseDto>
        {
            new UserResponseDto
            {
                Name = userEntity.Name,
                Cpf = userEntity.Cpf,
                Email = userEntity.Email,
                Phone = userEntity.Phone
            }
        };

        _mapperMock.Setup(m => m.Map<IEnumerable<UserResponseDto>>(It.IsAny<IEnumerable<User>>()))
            .Returns(responseDtos);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("98765432100", result.First().Cpf);
    }

    [Fact]
    public async Task GetByCpfAsync_ShouldReturnUser_WhenExists()
    {
        // Arrange
        var userEntity = new User
        {
            Name = "Alice Smith",
            Cpf = "11223344556",
            Email = "alice@example.com",
            Phone = "1122334455"
        };

        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync();

        var responseDto = new UserResponseDto
        {
            Name = userEntity.Name,
            Cpf = userEntity.Cpf,
            Email = userEntity.Email,
            Phone = userEntity.Phone
        };

        _mapperMock.Setup(m => m.Map<UserResponseDto>(userEntity)).Returns(responseDto);

        // Act
        var result = await _repository.GetByCpfAsync("11223344556");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("11223344556", result.Cpf);
    }

    [Fact]
    public async Task GetByCpfAsync_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _repository.GetByCpfAsync("NON_EXISTENT");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateByCpfAsync_ShouldUpdateUser_WhenExists()
    {
        // Arrange
        var userEntity = new User
        {
            Name = "Bob Brown",
            Cpf = "22334455667",
            Email = "bob@example.com",
            Phone = "2233445566"
        };

        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync();

        var requestDto = new UserRequestDto
        {
            Name = "Updated Bob",
            Cpf = "22334455667", // Same CPF for update
            Email = "updatedbob@example.com",
            Phone = "9988776655"
        };

        var responseDto = new UserResponseDto
        {
            Name = requestDto.Name,
            Cpf = requestDto.Cpf,
            Email = requestDto.Email,
            Phone = requestDto.Phone
        };

        _mapperMock.Setup(m => m.Map<UserResponseDto>(userEntity)).Returns(responseDto);

        // Act
        var result = await _repository.UpdateByCpfAsync("22334455667", requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Bob", result.Name);
        Assert.Equal("updatedbob@example.com", result.Email);
    }

    [Fact]
    public async Task UpdateByCpfAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var requestDto = new UserRequestDto
        {
            Name = "Test User",
            Cpf = "00000000000",
            Email = "test@example.com",
            Phone = "0000000000"
        };

        // Act
        var result = await _repository.UpdateByCpfAsync("NON_EXISTENT", requestDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteByCpfAsync_ShouldDeleteUser_WhenExists()
    {
        // Arrange
        var userEntity = new User
        {
            Name = "Carol White",
            Cpf = "33445566778",
            Email = "carol@example.com",
            Phone = "3344556677"
        };

        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteByCpfAsync("33445566778");

        // Assert
        Assert.True(result);
        Assert.Empty(_context.Users);
    }

    [Fact]
    public async Task DeleteByCpfAsync_ShouldReturnFalse_WhenNotExists()
    {
        // Act
        var result = await _repository.DeleteByCpfAsync("NON_EXISTENT");

        // Assert
        Assert.False(result);
    }
}
