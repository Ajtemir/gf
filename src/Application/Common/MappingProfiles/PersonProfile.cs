using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(d => d.Id, c => c.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, c => c.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, c => c.MapFrom(s => s.LastName))
            .ForMember(d => d.PatronymicName, c => c.MapFrom(s => s.PatronymicName))
            .ForMember(d => d.Fullname, c => c.MapFrom(s => string.Join(' ', s.LastName, s.FirstName, s.PatronymicName)))
            .ForMember(d => d.Gender, c => c.MapFrom(s => s.Gender))
            .ForMember(d => d.BirthDate, c => c.MapFrom(s => s.BirthDate))
            .ForMember(d => d.Pin, c => c.MapFrom(s => s.Pin))
            .ForMember(d => d.ActualAddress, c => c.MapFrom(s => s.ActualAddress))
            .ForMember(d => d.RegisteredAddress, c => c.MapFrom(s => s.RegisteredAddress))
            .ForMember(d => d.PassportSeriesNumber, c => c.MapFrom(s => s.PassportSeriesNumber))
            .ForMember(d => d.AvatarId, c => c.MapFrom(s => s.AvatarId))
            ;
    }
}