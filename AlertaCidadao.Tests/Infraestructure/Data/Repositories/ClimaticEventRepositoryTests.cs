using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using AlertaCidadao.Domain.Entities;
using AlertaCidadao.Infraestructure.Data.AppData;
using AlertaCidadao.Infraestructure.Data.Repositories;

namespace AlertaCidadao.Tests.Infraestructure.Data.Repositories;

public class ClimaticEventRepositoryTests
{
    private readonly ApplicationContext _context;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ClimaticEventRepository _repository;

    public ClimaticEventRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationContext(options);
        _mapperMock = new Mock<IMapper>();

        _repository = new ClimaticEventRepository(_context, _mapperMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddClimaticEvent()
    {
        // Arrange
        var requestDto = new ClimaticEventRequestDto
        {
            EventCode = "EVT001",
            Type = "Rain",
            Description = "Heavy rain",
            EventTime = DateTime.UtcNow,
            Latitude = 10.0m,
            Longitude = 20.0m,
            RiskLevel = 3
        };

        var eventEntity = new ClimaticEvent
        {
            EventCode = requestDto.EventCode,
            Type = requestDto.Type,
            Description = requestDto.Description,
            EventTime = requestDto.EventTime,
            Latitude = requestDto.Latitude,
            Longitude = requestDto.Longitude,
            RiskLevel = requestDto.RiskLevel
        };

        var responseDto = new ClimaticEventResponseDto
        {
            EventCode = eventEntity.EventCode,
            Type = eventEntity.Type,
            Description = eventEntity.Description,
            EventTime = eventEntity.EventTime,
            Latitude = eventEntity.Latitude,
            Longitude = eventEntity.Longitude,
            RiskLevel = eventEntity.RiskLevel
        };

        _mapperMock.Setup(m => m.Map<ClimaticEvent>(requestDto)).Returns(eventEntity);
        _mapperMock.Setup(m => m.Map<ClimaticEventResponseDto>(eventEntity)).Returns(responseDto);

        // Act
        var result = await _repository.AddAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(requestDto.EventCode, result.EventCode);
        Assert.Equal(requestDto.Type, result.Type);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllClimaticEvents()
    {
        // Arrange
        var eventEntity = new ClimaticEvent
        {
            EventCode = "EVT002",
            Type = "Storm",
            Description = "Strong storm",
            EventTime = DateTime.UtcNow,
            Latitude = 15.0m,
            Longitude = 25.0m,
            RiskLevel = 4
        };

        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();

        var responseDtos = new List<ClimaticEventResponseDto>
        {
            new ClimaticEventResponseDto
            {
                EventCode = eventEntity.EventCode,
                Type = eventEntity.Type,
                Description = eventEntity.Description,
                EventTime = eventEntity.EventTime,
                Latitude = eventEntity.Latitude,
                Longitude = eventEntity.Longitude,
                RiskLevel = eventEntity.RiskLevel
            }
        };

        _mapperMock.Setup(m => m.Map<IEnumerable<ClimaticEventResponseDto>>(It.IsAny<IEnumerable<ClimaticEvent>>()))
            .Returns(responseDtos);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("EVT002", result.First().EventCode);
    }

    [Fact]
    public async Task GetByEventCodeAsync_ShouldReturnClimaticEvent_WhenExists()
    {
        // Arrange
        var eventEntity = new ClimaticEvent
        {
            EventCode = "EVT003",
            Type = "Snow",
            Description = "Heavy snow",
            EventTime = DateTime.UtcNow,
            Latitude = 30.0m,
            Longitude = 40.0m,
            RiskLevel = 5
        };

        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();

        var responseDto = new ClimaticEventResponseDto
        {
            EventCode = eventEntity.EventCode,
            Type = eventEntity.Type,
            Description = eventEntity.Description,
            EventTime = eventEntity.EventTime,
            Latitude = eventEntity.Latitude,
            Longitude = eventEntity.Longitude,
            RiskLevel = eventEntity.RiskLevel
        };

        _mapperMock.Setup(m => m.Map<ClimaticEventResponseDto>(eventEntity)).Returns(responseDto);

        // Act
        var result = await _repository.GetByEventCodeAsync("EVT003");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("EVT003", result.EventCode);
    }

    [Fact]
    public async Task GetByEventCodeAsync_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _repository.GetByEventCodeAsync("NON_EXISTENT");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateByEventCodeAsync_ShouldUpdateClimaticEvent_WhenExists()
    {
        // Arrange
        var eventEntity = new ClimaticEvent
        {
            EventCode = "EVT004",
            Type = "Fog",
            Description = "Dense fog",
            EventTime = DateTime.UtcNow,
            Latitude = 35.0m,
            Longitude = 45.0m,
            RiskLevel = 2
        };

        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();

        var requestDto = new ClimaticEventRequestDto
        {
            Type = "Updated Fog",
            Description = "Updated description",
            EventTime = DateTime.UtcNow.AddHours(1),
            Latitude = 36.0m,
            Longitude = 46.0m,
            RiskLevel = 3
        };

        var responseDto = new ClimaticEventResponseDto
        {
            EventCode = eventEntity.EventCode,
            Type = requestDto.Type,
            Description = requestDto.Description,
            EventTime = requestDto.EventTime,
            Latitude = requestDto.Latitude,
            Longitude = requestDto.Longitude,
            RiskLevel = requestDto.RiskLevel
        };

        _mapperMock.Setup(m => m.Map<ClimaticEventResponseDto>(eventEntity)).Returns(responseDto);

        // Act
        var result = await _repository.UpdateByEventCodeAsync("EVT004", requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Fog", result.Type);
        Assert.Equal("Updated description", result.Description);
    }

    [Fact]
    public async Task UpdateByEventCodeAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var requestDto = new ClimaticEventRequestDto
        {
            Type = "Test",
            Description = "Test desc",
            EventTime = DateTime.UtcNow,
            Latitude = 0.0m,
            Longitude = 0.0m,
            RiskLevel = 1
        };

        // Act
        var result = await _repository.UpdateByEventCodeAsync("NON_EXISTENT", requestDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteByEventCodeAsync_ShouldDeleteClimaticEvent_WhenExists()
    {
        // Arrange
        var eventEntity = new ClimaticEvent
        {
            EventCode = "EVT005",
            Type = "Hail",
            Description = "Hailstorm",
            EventTime = DateTime.UtcNow,
            Latitude = 50.0m,
            Longitude = 60.0m,
            RiskLevel = 4
        };

        _context.Events.Add(eventEntity);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteByEventCodeAsync("EVT005");

        // Assert
        Assert.True(result);
        Assert.Empty(_context.Events);
    }

    [Fact]
    public async Task DeleteByEventCodeAsync_ShouldReturnFalse_WhenNotExists()
    {
        // Act
        var result = await _repository.DeleteByEventCodeAsync("NON_EXISTENT");

        // Assert
        Assert.False(result);
    }
}
