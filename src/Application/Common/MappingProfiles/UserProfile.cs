using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>()
            .ForMember(d => d.CreatedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.CreatedByUser!.LastName, s.CreatedByUser.FirstName, s.CreatedByUser.PatronymicName)))
            .ForMember(d => d.ModifiedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.ModifiedByUser!.LastName, s.ModifiedByUser.FirstName,
                    s.ModifiedByUser.PatronymicName)))
            .ForMember(d => d.Offices, c => c.MapFrom(s => s
                .UserOffices.Select(userOffice => new UserOfficeDto()
                {
                    OfficeId = userOffice.OfficeId,
                    NameRu = userOffice.Office!.NameRu,
                    NameKg = userOffice.Office!.NameKg,
                })))
            .ForMember(d => d.Image, c => c.MapFrom(s =>
                s.Image == null ? null : Convert.ToBase64String(s.Image)))
            .ForMember(d => d.Roles, c => c.MapFrom(s =>
                s.UserRoles.Select(r => r.Role.Name)));

        CreateMap<ApplicationUser, UserSummaryDto>()
            .ForMember(d => d.CreatedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.CreatedByUser!.LastName, s.CreatedByUser.FirstName, s.CreatedByUser.PatronymicName)))
            .ForMember(d => d.ModifiedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.ModifiedByUser!.LastName, s.ModifiedByUser.FirstName,
                    s.ModifiedByUser.PatronymicName)));
    }
}