using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PersonCandidate : Candidate
{
    [ForeignKey(nameof(Person))]
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public string? PositionRu { get; set; }
    public string? PositionKg { get; set; }
    
}