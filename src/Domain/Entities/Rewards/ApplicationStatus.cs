using Domain.Enums;

namespace Domain.Entities;

public class ApplicationStatus
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public int? PreviousStatusId { get; set; }
    public ApplicationStatus? PreviousStatus { get; set; }
    public int OfficeId { get; set; }
    public Office? Office { get; set; }
    public Application Application { get; set; } = null!;
    public ApplicationStatusType Status { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
}