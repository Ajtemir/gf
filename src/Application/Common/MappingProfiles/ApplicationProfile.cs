using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<RewardApplication, ApplicationDto>()
            .ForMember(d => d.SpecialAchievements, c => c.MapFrom(s => s.SpecialAchievements))
            .ForMember(d => d.Candidate, c => c.MapFrom(x=>x.Candidate))
            .ForMember(d => d.Id, c => c.MapFrom(x=>x.Id))
            .ForMember(d => d.Documents, c => c.MapFrom(x=>x.Documents))
            .ForMember(d => d.Statuses, c => c.MapFrom(x=>x.RewardApplicationStatuses))
            .ForMember(d => d.Reward, c => c.MapFrom(x=>x.Reward))
            ;
    }
}