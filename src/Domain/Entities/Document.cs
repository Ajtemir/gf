namespace Domain.Entities;

public class Document
{
    public int Id { get; set; }
    public required byte[] Bytes { get; set; }
    public required string Name { get; set; }
    public required string Extension { get; set; }
    public int DocumentTypeId { get; set; }
    public required DocumentType DocumentType { get; set; }
}