using AlertaCidadao.Application.Services.Interfaces;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using AlertaCidadao.Infraestructure.Data.Repositories;
using AlertaCidadao.Infraestructure.Data.Repositories.Interfaces;

namespace AlertaCidadao.Application.Services
{
    public class ClimaticEventService : IClimaticEventService
    {
        private readonly IClimaticEventRepository _eventRepository;

        public ClimaticEventService(IClimaticEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ClimaticEventResponseDto> AddEventAsync(ClimaticEventRequestDto requestDto)
        {
            return await _eventRepository.AddAsync(requestDto);
        }

        public async Task<IEnumerable<ClimaticEventResponseDto>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<ClimaticEventResponseDto?> GetEventByEventCodeAsync(string eventCode)
        {
            return await _eventRepository.GetByEventCodeAsync(eventCode);
        }

        public async Task<ClimaticEventResponseDto?> UpdateEventByEventCodeAsync(string eventCode, ClimaticEventRequestDto requestDto)
        {
            return await _eventRepository.UpdateByEventCodeAsync(eventCode, requestDto);
        }

        public async Task<bool> DeleteEventByEventCodeAsync(string eventCode)
        {
            return await _eventRepository.DeleteByEventCodeAsync(eventCode);
        }
    }
}
