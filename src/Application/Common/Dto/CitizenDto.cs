using Domain.Enums;

namespace Application.Common.Dto;

public class CitizenDto : PersonCandidateDto
{
    
    public int YearsOfWorkTotal { get; set; }
    public int YearsOfWorkInIndustry { get; set; }
    public int YearsOfWorkInCollective { get; set; }
    public int EducationId { get; set; }
    public string? Education { get; set; }
    public string? ScienceDegree { get; set; }
    
}