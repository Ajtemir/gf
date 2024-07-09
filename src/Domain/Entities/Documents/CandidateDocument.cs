using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;
[PrimaryKey(nameof(CandidateId), nameof(DocumentId))]
public class CandidateDocument
{
    [ForeignKey(nameof(Candidate))]
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; } = null!;
    [ForeignKey(nameof(Document))]
    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;
}