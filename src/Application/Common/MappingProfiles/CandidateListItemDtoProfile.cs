using Application.Common.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MappingProfiles;

public class CandidateListItemDtoProfile : Profile
{
    public CandidateListItemDtoProfile()
    {
        CreateMap<Candidate, CandidateListItemDto>().ConvertUsing(x => x is PersonCandidate
            ? new CandidateListItemDto
            {
                Id = x.Id,
                Pin = (x as PersonCandidate)!.Person.Pin,
                Name = (x as PersonCandidate)!.Person.Fullname,
                CreatedAt = x.CreatedAt,
                CandidateType = x.CandidateTypeId
            } 
            : new CandidateListItemDto
            {
                Id = x.Id,
                Pin = (x as Entity)!.PinEntity.Pin,
                Name = (x as Entity)!.NameRu,
                CreatedAt = x.CreatedAt,
                CandidateType = x.CandidateTypeId
            }
        );
    }
}