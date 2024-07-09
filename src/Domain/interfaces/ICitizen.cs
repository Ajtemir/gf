using Domain.Dictionary;

namespace Domain.interfaces;

public interface ICitizen
{
    public int? EducationId { get; set; }
    public string? ScienceDegree { get; set; }
    public int YearsOfWorkTotal { get; set; }
    public int YearsOfWorkInCollective { get; set; }
    public int YearsOfWorkInIndustry { get; set; }
}