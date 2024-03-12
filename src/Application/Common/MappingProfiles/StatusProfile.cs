using AutoMapper;
using Domain.Entities;
using Microsoft.OpenApi.Extensions;

namespace WebAPI.Controllers;

public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<ApplicationStatus, StatusDto>()
            .ForMember(d => d.Id, s => s.MapFrom(x => x.Id))
            .ForMember(d => d.StatusName, s => s.MapFrom(x=>x.Status.GetDisplayName()))
            .ForMember(d=>d.OfficeId, s => s.MapFrom(x=>x.OfficeId))
            .ForMember(d=>d.Office, s => s.MapFrom(x=>x.Office))
            .ForMember(d => d.ChangeTime, s => s.MapFrom(x=>x.ChangeDate))
            .ForMember(d=>d.UserId, s=>s.MapFrom(x=> 1)) // todo
            .ForMember(d=>d.ApplicationStatusType, s=>s.MapFrom(x=> x.Status)) // todo
            ;
    }
}