using Application.Common.Dto;
using Application.Common.Extensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.OpenApi.Extensions;

namespace WebAPI.Controllers;

public class StatusDto
{
    public int Id { get; set; }
    public CandidateStatusType CandidateStatusType { get; set; }
    public string StatusName => CandidateStatusType.GetDisplayName();
    public DateTime ChangeTime { get; set; }
    public int OfficeId { get; set; }
    public OfficeDto Office { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; } = new UserDto
    {
        Id = 1,
        UserName = "Admin",
        LastName = "Adminov",
        FirstName = "Admin",
        CreatedAt = DateTime.Now.SetKindUtc(),
        ModifiedAt = DateTime.Now.SetKindUtc()
    };
}