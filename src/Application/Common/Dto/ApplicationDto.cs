using Domain.Entities;
using WebAPI.Controllers;

namespace Application.Common.Dto;

public class ApplicationDto
{
    public int CandidateId { get; set; }
    public CandidateDto Candidate { get; set; }
    public int Id { get; set; }
    public List<StatusDto> Statuses { get; set; }
    public List<DocumentDto> Documents { get; set; }
    public int RewardId { get; set; }
    public RewardDto Reward { get; set; }
    public string SpecialAchievements { get; set; }
}