using AutoMapper;
using AlertaCidadao.Contracts.Dtos.Requests;
using AlertaCidadao.Contracts.Dtos.Responses;
using AlertaCidadao.Domain.Entities;

namespace AlertaCidadao.Infraestructure.Mappings;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserRequestDto>().ReverseMap();
        CreateMap<User, UserResponseDto>().ReverseMap();

        CreateMap<ClimaticEvent, ClimaticEventRequestDto>().ReverseMap();
        CreateMap<ClimaticEvent, ClimaticEventResponseDto>().ReverseMap();

        CreateMap<SafeResource, SafeResourceRequestDto>().ReverseMap();
        CreateMap<SafeResource, SafeResourceResponseDto>().ReverseMap();
    }
}
