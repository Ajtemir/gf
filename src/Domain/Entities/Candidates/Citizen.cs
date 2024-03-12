using System.ComponentModel.DataAnnotations.Schema;
using Domain.Dictionary;

namespace Domain.Entities;

public class Citizen : PersonCandidate
{
    public int EducationId { get; set; }
    public Education? Education { get; set; }
    public string? ScienceDegree { get; set; }
    
    public int YearsOfWorkTotal { get; set; }
    public int YearsOfWorkInIndustry { get; set; }
    public int YearsOfWorkInCollective { get; set; }
}