using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MothersChildren
{
    [ForeignKey(nameof(Mother))]
    [Column(Order = 1)]
    [Key]
    public int MotherId { get; set; }
    public Mother Mother { get; set; } = null!;
    [ForeignKey(nameof(Child))]
    [Column(Order = 2)]
    [Key]
    public int ChildId { get; set; }
    public Child Child { get; set; } = null!;
}