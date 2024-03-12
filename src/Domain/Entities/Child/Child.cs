using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

[Table("Children")]
public class Child
{
    public int Id { get; set; }
    [ForeignKey(nameof(Person))]
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
    public MotherChild MotherChild { get; set; } = null!;
    [NotMapped]
    public string? Pin => Person?.Pin;
    public ICollection<ChildDocument> ChildDocuments { get; set; } = new List<ChildDocument>();
    public bool IsAdopted { get; set; }
}