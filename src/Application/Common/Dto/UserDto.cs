namespace Application.Common.Dto;

public class UserDto
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public string? Email { get; set; }
    public string? Pin { get; set; }
    public string? Image { get; set; }

    public int? CreatedBy { get; set; }
    public string? CreatedByUser { get; set; }
    public required DateTime CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }
    public string? ModifiedByUser { get; set; }
    public required DateTime ModifiedAt { get; set; }

    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<UserOfficeDto> Offices { get; set; } = Enumerable.Empty<UserOfficeDto>();
}