namespace Domain.Entities;

public class Avatar
{
    public int Id { get; set; }
    public byte[]? Image { get; set; }
    public string? ImageName { get; set; }
}