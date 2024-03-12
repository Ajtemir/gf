using System.Linq.Expressions;
using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class CandidateWithoutImageProfile : Profile
{
    public CandidateWithoutImageProfile()
    {
        CreateMap<Candidate, CandidateWithoutImageDto>()
            .ForMember(d => d.Name, c => c.Ignore())
            .Include<Citizen, CandidateWithoutImageDto>()
            .Include<Mother, CandidateWithoutImageDto>()
            .Include<Foreigner, CandidateWithoutImageDto>()
            .Include<Entity, CandidateWithoutImageDto>();
        

        CreateMap<Citizen, CandidateWithoutImageDto>()
            .ForMember(d => d.Name, c => c.MapFrom(MapPersonName<Citizen>()));

        CreateMap<Mother, CandidateWithoutImageDto>()
            .ForMember(d => d.Name, c => c.MapFrom(MapPersonName<Mother>()));


        CreateMap<Foreigner, CandidateWithoutImageDto>()
            .ForMember(d => d.Name, c => c.MapFrom(MapPersonName<Foreigner>()));

        CreateMap<Entity, CandidateWithoutImageDto>()
            .ForMember(d => d.Name, c => c.MapFrom(s => s.NameRu));
    }
    
    private static Expression<Func<TPerson, string>> MapPersonName<TPerson>() where TPerson : PersonCandidate =>
        person => string.Join(' ', new []
                {
                    person.Person.LastName,
                    person.Person.FirstName,
                    person.Person.PatronymicName
                }
                .OfType<string>());
}