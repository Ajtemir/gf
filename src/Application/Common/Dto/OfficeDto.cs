namespace Application.Common.Dto;

public class OfficeDto
{
    public int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    public IList<OfficeDto> ChildOffices { get; set; } = new List<OfficeDto>();
    public IList<OfficeDto> ParentOffices { get; set; } = new List<OfficeDto>();

    public int CreatedBy { get; set; }
    public string? CreatedByUser { get; set; }
    public DateTime CreatedAt { get; set; }

    public int ModifiedBy { get; set; }
    public string? ModifiedByUser { get; set; }
    public DateTime ModifiedAt { get; set; }
}
