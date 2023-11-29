using Application.Common.Dto;
using AutoMapper;
using Domain.Dictionary;

namespace Application.Common.MappingProfiles;

public class DictionaryProfile : Profile
{
    public DictionaryProfile()
    {
        CreateMap<Position, PositionDto>();
        CreateMap<Education, EducationDto>();
        CreateMap<Citizenship, CitizenshipDto>();
    }
}