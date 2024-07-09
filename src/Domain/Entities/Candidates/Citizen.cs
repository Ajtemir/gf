using Domain.Dictionary;
using Domain.interfaces;

namespace Domain.Entities;

public class Citizen : PersonCandidate, ICitizen, IUpdate<ICitizen>
{
    public int? EducationId { get; set; }
    public Education? Education { get; set; }
    public string? ScienceDegree { get; set; }
    
    public int YearsOfWorkTotal { get; set; }
    public int YearsOfWorkInIndustry { get; set; }
    public int YearsOfWorkInCollective { get; set; }
    public void Update(ICitizen entity)
    {
        EducationId = entity.EducationId;
        ScienceDegree = entity.ScienceDegree;
        YearsOfWorkTotal = entity.YearsOfWorkTotal;
        YearsOfWorkInIndustry = entity.YearsOfWorkInIndustry;
        YearsOfWorkInCollective = entity.YearsOfWorkInCollective;
    }
}