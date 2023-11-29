using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<Office, OfficeDto>()
            .ForMember(d => d.ChildOffices, c => c.MapFrom(s => s.ChildOffices.Select(x => x.ChildOffice)))
            .ForMember(d => d.ParentOffices, c => c.MapFrom(s => s.ParentOffices.Select(x => x.ParentOffice)))
            .ForMember(d => d.CreatedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.CreatedByUser!.LastName, s.CreatedByUser.FirstName, s.CreatedByUser.PatronymicName)))
            .ForMember(d => d.ModifiedByUser, c => c.MapFrom(s =>
                string.Join(' ', s.ModifiedByUser!.LastName, s.ModifiedByUser.FirstName,
                    s.ModifiedByUser.PatronymicName)));

        CreateMap<OfficeDto, Office>()
            .ForMember(d => d.ChildOffices, s => s.Ignore())
            .ForMember(d => d.ParentOffices, s => s.Ignore())
            .ForMember(d => d.Users, s => s.Ignore())
            .ForMember(d => d.UserOffices, s => s.Ignore())
            .ForMember(d => d.DomainEvents, s => s.Ignore())
            .ForMember(d => d.CreatedBy, s => s.Ignore())
            .ForMember(d => d.CreatedAt, s => s.Ignore())
            .ForMember(d => d.CreatedByUser, s => s.Ignore())
            .ForMember(d => d.ModifiedBy, s => s.Ignore())
            .ForMember(d => d.ModifiedAt, s => s.Ignore())
            .ForMember(d => d.ModifiedByUser, s => s.Ignore())
            ;
    }
}