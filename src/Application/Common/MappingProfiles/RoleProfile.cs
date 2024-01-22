using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Common.MappingProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<DomainRole, ApplicationRole>()
            .ForMember(d => d.UserRoles, c => c.Ignore())
            .ForMember(d => d.NormalizedName, c => c.Ignore())
            .ForMember(d => d.ConcurrencyStamp, c => c.Ignore());
    }
}