namespace Application.Common.Dto;

public class PersonCandidateDto : CandidateDto
{
    public PersonDto Person { get; set; }
    public string Name { get; set; }
}