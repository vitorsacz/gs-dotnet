using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;

namespace AlertaCidadao.Infraestructure.Data.Repositories.Interfaces
{
    public interface ISafeResourceRepository
    {
        Task<SafeResourceResponseDto> AddAsync(SafeResourceRequestDto requestDto);
        Task<bool> DeleteByResourceCodeAsync(string resourceCode);
        Task<IEnumerable<SafeResourceResponseDto>> GetAllAsync();
        Task<SafeResourceResponseDto?> GetByResourceCodeAsync(string resourceCode);
        Task<SafeResourceResponseDto?> UpdateByResourceCodeAsync(string resourceCode, SafeResourceRequestDto requestDto);
    }
}