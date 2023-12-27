using Domain.Enums;

namespace Domain.Entities;

public class RewardApplicationStatus
{
    public int Id { get; set; }
    public int RewardApplicationId { get; set; }
    public RewardApplication RewardApplication { get; set; }
    public RewardApplicationStatusType Status { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.Now;
}