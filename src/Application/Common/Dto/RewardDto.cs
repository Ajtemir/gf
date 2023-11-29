namespace Application.Common.Dto;

public class RewardDto
{
    public int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public required string ImageName { get; set; }
    public required string Image { get; set; }
    
    public int CreatedBy { get; set; }
    public string? CreatedByUser { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ModifiedBy { get; set; }
    public string? ModifiedByUser { get; set; }
    public DateTime ModifiedAt { get; set; }
}