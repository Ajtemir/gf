using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class RewardProfile : Profile
{
    public RewardProfile()
    {
        CreateMap<Reward, RewardDto>()
            .ForMember(d => d.Image, c => c.MapFrom(s =>
                Convert.ToBase64String(s.Image)))
            .ForMember(d => d.CreatedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.CreatedByUser!.LastName, s.CreatedByUser.FirstName, s.CreatedByUser.PatronymicName)))
            .ForMember(d => d.ModifiedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.ModifiedByUser!.LastName, s.ModifiedByUser.FirstName,
                    s.ModifiedByUser.PatronymicName)));
    }
}