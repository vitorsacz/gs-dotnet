using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using AlertaCidadao.Domain.Entities;
using AlertaCidadao.Infraestructure.Data.AppData;
using AlertaCidadao.Infraestructure.Data.Repositories.Interfaces;

namespace AlertaCidadao.Infraestructure.Data.Repositories;

public class SafeResourceRepository : ISafeResourceRepository
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public SafeResourceRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SafeResourceResponseDto> AddAsync(SafeResourceRequestDto requestDto)
    {
        var resource = _mapper.Map<SafeResource>(requestDto);
        _context.Add(resource);
        await _context.SaveChangesAsync();
        return _mapper.Map<SafeResourceResponseDto>(resource);
    }

    public async Task<IEnumerable<SafeResourceResponseDto>> GetAllAsync()
    {
        var resources = await _context.SafeResources.ToListAsync();
        return _mapper.Map<IEnumerable<SafeResourceResponseDto>>(resources);
    }

    public async Task<SafeResourceResponseDto?> GetByResourceCodeAsync(string resourceCode)
    {
        var resource = await _context.SafeResources.FirstOrDefaultAsync(r => r.ResourceCode == resourceCode);
        if (resource == null) return null;
        return _mapper.Map<SafeResourceResponseDto>(resource);
    }

    public async Task<SafeResourceResponseDto?> UpdateByResourceCodeAsync(string resourceCode, SafeResourceRequestDto requestDto)
    {
        var resource = await _context.SafeResources.FirstOrDefaultAsync(r => r.ResourceCode == resourceCode);
        if (resource == null) return null;

        resource.Name = requestDto.Name;
        resource.Description = requestDto.Description;
        resource.Latitude = requestDto.Latitude;
        resource.Longitude = requestDto.Longitude;
        resource.Capacity = requestDto.Capacity;

        await _context.SaveChangesAsync();
        return _mapper.Map<SafeResourceResponseDto>(resource);
    }

    public async Task<bool> DeleteByResourceCodeAsync(string resourceCode)
    {
        var resource = await _context.SafeResources.FirstOrDefaultAsync(r => r.ResourceCode == resourceCode);
        if (resource == null) return false;

        _context.SafeResources.Remove(resource);
        await _context.SaveChangesAsync();
        return true;
    }
}
