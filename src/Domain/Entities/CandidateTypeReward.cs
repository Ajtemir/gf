using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey(nameof(CandidateTypeId), nameof(RewardId))]
public class CandidateTypeReward
{
    [ForeignKey(nameof(CandidateType))]
    public required string CandidateTypeId { get; set; }
    public CandidateType CandidateType { get; set; } = null!;
    [ForeignKey(nameof(Reward))]
    public required int RewardId { get; set; }
    public Reward Reward { get; set; } = null!;
}