using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class ChildProfile : Profile
{
    public ChildProfile()
    {
        CreateMap<Child, ChildListItemDto>()
            .ForMember(d => d.Id, c => c.MapFrom(x=>x.Id))
            .ForMember(d => d.Gender, c => c.MapFrom(x=>x.Person.Gender))
            .ForMember(d => d.FullName, c => c.MapFrom(x => string.Join(' ', x.Person.LastName, x.Person.FirstName, x.Person.PatronymicName)))
            ;
    }
}