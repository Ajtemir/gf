using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class MemberType
{
    [Key]
    public string Id { get; set; }

    public static string Individual => "Person";
    public static string Legal => "PinEntity";
}