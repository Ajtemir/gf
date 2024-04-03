namespace Application.Common.Dto;

public class ChildInfoDto
{
    public int Id { get; set; }
    public int MotherId { get; set; }
    public required PersonDto Person { get; set; }
}