using Domain.Enums;

namespace Application.Common.Dto;

public class MotherDto : CandidateDto
{
    public PersonDto Person { get; set; }
}