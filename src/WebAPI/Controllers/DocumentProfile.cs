using AutoMapper;
using Domain.Entities;

namespace WebAPI.Controllers;

public class DocumentProfile : Profile
{
    public DocumentProfile()
    {
        CreateMap<Document, DocumentDto>()
            .ForMember(d => d.Id, s => s.MapFrom(x=>x.Id))
            .ForMember(d => d.IsRequired, s => s.MapFrom(x=>x.DocumentType.Required))
            .ForMember(d=> d.DocumentTypeName, s=>s.MapFrom(x=>x.DocumentType.NameRu))
            .ForMember(d=> d.Name, s=>s.MapFrom(x=>x.Name))
            ;
    }
}