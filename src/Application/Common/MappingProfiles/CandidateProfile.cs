using System.Linq.Expressions;
using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class CandidateProfile : Profile
{
    public CandidateProfile()
    {
        CreateMap<Candidate, CandidateDto>()
            .Include<Citizen, CitizenDto>()
            .Include<Mother, MotherDto>()
            .Include<Foreigner, ForeignerDto>()
            .Include<Entity, EntityDto>()
            .ForMember(d => d.CandidateType, c => c.MapFrom(s => s.CandidateTypeId))
            .ForMember(d => d.Image, c => c.MapFrom(s => s.Image == null ? null : Convert.ToBase64String(s.Image)))
            .ForMember(d => d.CreatedByUser, c => c.MapFrom(MapCreatedByUser()))
            .ForMember(d => d.ApplicationId, c => c.MapFrom(s => s.Application!.Id))
            .ForMember(d => d.ModifiedByUser, c => c.MapFrom(MapModifiedByUser()));
        
        CreateMap<Citizen, CitizenDto>()
            .ForMember(d => d.Education, c => c.MapFrom(s =>
                s.Education!.NameRu))
            .ForMember(d => d.Person, c => c.MapFrom(x=>x.Person))
            ;
        
        CreateMap<Foreigner, ForeignerDto>()
            .ForMember(d => d.Person, c => c.MapFrom(x=>x.Person))
            .ForMember(d => d.CitizenshipRu, c => c.MapFrom(s => s.Citizenship!.NameRu))
            .ForMember(d => d.CitizenshipKg, c => c.MapFrom(s => s.Citizenship!.NameKg));

        CreateMap<Mother, MotherDto>()
            .ForMember(d => d.Person, c => c.MapFrom(x => x.Person))
            ;
        CreateMap<Entity, EntityDto>();
    }

    private static Expression<Func<Candidate, string?>> MapCreatedByUser() =>
        candidate =>
            string.Join(' ',
                new[]
                {
                    candidate.CreatedByUser!.LastName,
                    candidate.CreatedByUser!.FirstName,
                    candidate.CreatedByUser!.PatronymicName
                }.OfType<string>());

    private static Expression<Func<Candidate, string?>> MapModifiedByUser() =>
        candidate =>
            string.Join(' ',
                new[]
                {
                    candidate.ModifiedByUser!.LastName,
                    candidate.ModifiedByUser!.FirstName,
                    candidate.ModifiedByUser!.PatronymicName
                }.OfType<string>());
}