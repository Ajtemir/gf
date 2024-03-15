using Domain.Enums;

namespace Application.Common.Dto;

public class ChildListItemDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public Gender Gender { get; set; }
}