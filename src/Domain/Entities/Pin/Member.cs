using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Pin), IsUnique = true)]
public class Member
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Pin { get; set; }
    public int? AvatarId { get; set; }
    public Avatar? Avatar { get; set; }
    public string MemberTypeId { get; set; } = null!;
    public MemberType MemberType { get; set; } = null!;
}