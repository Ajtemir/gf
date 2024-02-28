namespace WebAPI.Controllers;

public class DocumentDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string DocumentTypeName { get; set; }
    public bool IsRequired { get; set; }
}