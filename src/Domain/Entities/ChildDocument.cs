using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;
[PrimaryKey(nameof(ChildId), nameof(DocumentId))]
public class ChildDocument
{
    [ForeignKey(nameof(Child))]
    public int ChildId { get; set; } 
    public Child Child { get; set; } = null!;
    [ForeignKey(nameof(Document))]
    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;
}