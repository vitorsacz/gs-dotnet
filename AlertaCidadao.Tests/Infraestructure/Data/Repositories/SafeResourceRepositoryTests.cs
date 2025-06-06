using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using AlertaCidadao.Domain.Entities;
using AlertaCidadao.Infraestructure.Data.AppData;
using AlertaCidadao.Infraestructure.Data.Repositories;

namespace AlertaCidadao.Tests.Infraestructure.Data.Repositories;

public class SafeResourceRepositoryTests
{
    private readonly ApplicationContext _context;
    private readonly Mock<IMapper> _mapperMock;
    private readonly SafeResourceRepository _repository;

    public SafeResourceRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationContext(options);
        _mapperMock = new Mock<IMapper>();

        _repository = new SafeResourceRepository(_context, _mapperMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddSafeResource()
    {
        // Arrange
        var requestDto = new SafeResourceRequestDto
        {
            ResourceCode = "RES001",
            Name = "Shelter 1",
            Description = "Description 1",
            Latitude = 10.0m,
            Longitude = 20.0m,
            Capacity = 100
        };

        var resourceEntity = new SafeResource
        {
            ResourceCode = requestDto.ResourceCode,
            Name = requestDto.Name,
            Description = requestDto.Description,
            Latitude = requestDto.Latitude,
            Longitude = requestDto.Longitude,
            Capacity = requestDto.Capacity
        };

        var responseDto = new SafeResourceResponseDto
        {
            ResourceCode = resourceEntity.ResourceCode,
            Name = resourceEntity.Name,
            Description = resourceEntity.Description,
            Latitude = resourceEntity.Latitude,
            Longitude = resourceEntity.Longitude,
            Capacity = resourceEntity.Capacity
        };

        _mapperMock.Setup(m => m.Map<SafeResource>(requestDto)).Returns(resourceEntity);
        _mapperMock.Setup(m => m.Map<SafeResourceResponseDto>(resourceEntity)).Returns(responseDto);

        // Act
        var result = await _repository.AddAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(requestDto.ResourceCode, result.ResourceCode);
        Assert.Equal(requestDto.Name, result.Name);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSafeResources()
    {
        // Arrange
        var resourceEntity = new SafeResource
        {
            ResourceCode = "RES002",
            Name = "Shelter 2",
            Description = "Description 2",
            Latitude = 15.0m,
            Longitude = 25.0m,
            Capacity = 150
        };

        _context.SafeResources.Add(resourceEntity);
        await _context.SaveChangesAsync();

        var responseDtos = new List<SafeResourceResponseDto>
        {
            new SafeResourceResponseDto
            {
                ResourceCode = resourceEntity.ResourceCode,
                Name = resourceEntity.Name,
                Description = resourceEntity.Description,
                Latitude = resourceEntity.Latitude,
                Longitude = resourceEntity.Longitude,
                Capacity = resourceEntity.Capacity
            }
        };

        _mapperMock.Setup(m => m.Map<IEnumerable<SafeResourceResponseDto>>(It.IsAny<IEnumerable<SafeResource>>()))
            .Returns(responseDtos);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("RES002", result.First().ResourceCode);
    }

    [Fact]
    public async Task GetByResourceCodeAsync_ShouldReturnSafeResource_WhenExists()
    {
        // Arrange
        var resourceEntity = new SafeResource
        {
            ResourceCode = "RES003",
            Name = "Shelter 3",
            Description = "Description 3",
            Latitude = 20.0m,
            Longitude = 30.0m,
            Capacity = 200
        };

        _context.SafeResources.Add(resourceEntity);
        await _context.SaveChangesAsync();

        var responseDto = new SafeResourceResponseDto
        {
            ResourceCode = resourceEntity.ResourceCode,
            Name = resourceEntity.Name,
            Description = resourceEntity.Description,
            Latitude = resourceEntity.Latitude,
            Longitude = resourceEntity.Longitude,
            Capacity = resourceEntity.Capacity
        };

        _mapperMock.Setup(m => m.Map<SafeResourceResponseDto>(resourceEntity)).Returns(responseDto);

        // Act
        var result = await _repository.GetByResourceCodeAsync("RES003");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("RES003", result.ResourceCode);
    }

    [Fact]
    public async Task GetByResourceCodeAsync_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _repository.GetByResourceCodeAsync("NON_EXISTENT");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateByResourceCodeAsync_ShouldUpdateSafeResource_WhenExists()
    {
        // Arrange
        var resourceEntity = new SafeResource
        {
            ResourceCode = "RES004",
            Name = "Shelter 4",
            Description = "Description 4",
            Latitude = 25.0m,
            Longitude = 35.0m,
            Capacity = 250
        };

        _context.SafeResources.Add(resourceEntity);
        await _context.SaveChangesAsync();

        var requestDto = new SafeResourceRequestDto
        {
            Name = "Updated Shelter",
            Description = "Updated description",
            Latitude = 26.0m,
            Longitude = 36.0m,
            Capacity = 300
        };

        var responseDto = new SafeResourceResponseDto
        {
            ResourceCode = resourceEntity.ResourceCode,
            Name = requestDto.Name,
            Description = requestDto.Description,
            Latitude = requestDto.Latitude,
            Longitude = requestDto.Longitude,
            Capacity = requestDto.Capacity
        };

        _mapperMock.Setup(m => m.Map<SafeResourceResponseDto>(resourceEntity)).Returns(responseDto);

        // Act
        var result = await _repository.UpdateByResourceCodeAsync("RES004", requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Shelter", result.Name);
        Assert.Equal("Updated description", result.Description);
    }

    [Fact]
    public async Task UpdateByResourceCodeAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var requestDto = new SafeResourceRequestDto
        {
            Name = "Test",
            Description = "Test description",
            Latitude = 0.0m,
            Longitude = 0.0m,
            Capacity = 50
        };

        // Act
        var result = await _repository.UpdateByResourceCodeAsync("NON_EXISTENT", requestDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteByResourceCodeAsync_ShouldDeleteSafeResource_WhenExists()
    {
        // Arrange
        var resourceEntity = new SafeResource
        {
            ResourceCode = "RES005",
            Name = "Shelter 5",
            Description = "Description 5",
            Latitude = 30.0m,
            Longitude = 40.0m,
            Capacity = 350
        };

        _context.SafeResources.Add(resourceEntity);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.DeleteByResourceCodeAsync("RES005");

        // Assert
        Assert.True(result);
        Assert.Empty(_context.SafeResources);
    }

    [Fact]
    public async Task DeleteByResourceCodeAsync_ShouldReturnFalse_WhenNotExists()
    {
        // Act
        var result = await _repository.DeleteByResourceCodeAsync("NON_EXISTENT");

        // Assert
        Assert.False(result);
    }
}
