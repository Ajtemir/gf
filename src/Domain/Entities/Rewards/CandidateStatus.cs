using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class CandidateStatus
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public int? PreviousStatusId { get; set; }
    public CandidateStatus? PreviousStatus { get; set; }
    public int OfficeId { get; set; }
    public Office? Office { get; set; }
    [ForeignKey(nameof(CandidateId))]
    public Candidate Candidate { get; set; } = null!;
    public CandidateStatusType Status { get; set; }
    public DateTime ChangeDate { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public ApplicationUser User { get; set; }
}