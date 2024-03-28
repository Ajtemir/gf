using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class AvatarProfile : Profile
{
    public AvatarProfile()
    {
        CreateMap<Avatar, AvatarDto>()
            .ForMember(d => d.Id, c => c.MapFrom(s => s.Id))
            .ForMember(d => d.Image, c => c.MapFrom(x=>x.Image64))
            .ForMember(d => d.ImageName, c => c.MapFrom(x=>x.ImageName))
            ;
    }
}