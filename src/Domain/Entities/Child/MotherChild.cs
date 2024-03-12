using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey(nameof(MotherId), nameof(ChildId))]
public class MotherChild
{
    [ForeignKey(nameof(Mother))]
    public int MotherId { get; set; }
    public Mother Mother { get; set; } = null!;
    [ForeignKey(nameof(Child))]
    public int ChildId { get; set; }
    public Child Child { get; set; } = null!;
}