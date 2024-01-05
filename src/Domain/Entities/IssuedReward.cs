using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class IssuedReward
{
    [Key,ForeignKey((nameof(RewardApplicationStatus)))]
    public int RewardApplicationStatusId { get; set; }
    public RewardApplicationStatus? RewardApplicationStatus { get; set; }
}