using Domain.Enums;

namespace Application.Common.Dto;

public class ForeignerDto : CandidateDto
{
    public PersonDto Person { get; set; }
    public int CitizenshipId { get; set; }
    public string? CitizenshipRu { get; set; }
    public string? CitizenshipKg { get; set; }
}