using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

[Table("Children")]
public class Child
{
    public int Id { get; set; }
    
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    
    public required GenderType Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    
    public string? BirthPlace { get; set; }
    public string? WorkPlace { get; set; }
    public string? StudyPlace { get; set; }
    public string? RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
    public string? PassportSeriesNumber { get; set; }
    
    public MotherChild MotherChild { get; set; } = null!;
    public byte[]? Image { get; set; }
    public string? ImageName { get; set; }
    public int MemberId { get; set; }
    public Member? Member { get; set; }
    [NotMapped]
    public string? Pin => Member?.Pin;
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<ChildDocument> ChildDocuments { get; set; } = new List<ChildDocument>();
    public bool IsAdopted { get; set; }
}