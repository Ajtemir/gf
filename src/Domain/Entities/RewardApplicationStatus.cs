using Domain.Enums;

namespace Domain.Entities;

public class RewardApplicationStatus
{
    public int Id { get; set; }
    public int RewardApplicationId { get; set; }
    public int? PreviousStatusId { get; set; }
    public RewardApplicationStatus? PreviousStatus { get; set; }
    public int OfficeId { get; set; }
    public Office? Office { get; set; }
    public RewardApplication RewardApplication { get; set; } = null!;
    public RewardApplicationStatusType Status { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
}