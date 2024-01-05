namespace Domain.Entities;

public class Mother : Person
{
    public required string? PassportNumber { get; set; }
    public required string RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
}