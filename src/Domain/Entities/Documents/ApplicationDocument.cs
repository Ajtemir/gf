using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;
[PrimaryKey(nameof(ApplicationId), nameof(DocumentId))]
public class ApplicationDocument
{
    [ForeignKey(nameof(Application))]
    public int ApplicationId { get; set; }
    public Application Application { get; set; } = null!;
    [ForeignKey(nameof(Document))]
    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;
}