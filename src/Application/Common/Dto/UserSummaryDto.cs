namespace Application.Common.Dto;

public class UserSummaryDto
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public string? CreatedByUser { get; set; }
    public required DateTime CreatedAt { get; set; }
    public string? ModifiedByUser { get; set; }
    public required DateTime ModifiedAt { get; set; }
}