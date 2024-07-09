using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class IssuedReward
{
    [Key,ForeignKey((nameof(CandidateStatus)))]
    public int CandidateStatusId { get; set; }
    public CandidateStatus? CandidateStatus { get; set; }
}