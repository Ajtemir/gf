using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey(nameof(RewardId), nameof(DocumentTypeId))]
public class RewardDocumentType
{
    [ForeignKey(nameof(Reward))]
    public int RewardId { get; set; }

    public Reward Reward { get; set; }
    [ForeignKey(nameof(DocumentType))]
    
    public int DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; }
}