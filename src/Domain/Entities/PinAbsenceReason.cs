using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PinAbsenceReason
{
    public int Id { get; set; }
    public required string Reason { get; set; }
    [ForeignKey(nameof(Member))]
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;
}