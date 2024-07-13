using Application.Common.Dto;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class MembersController
{
    [HttpPost]
    public async Task<ActionResult> CreatePersonMember([FromBody] CreatePersonMemberArgument argument)
    {
        if (argument.Pin != null)
            await _context.Members.ErrorIfExistsAsync(x => x.Pin == argument.Pin, errorMessage: $"Найдена запись с таким ПИНом : {argument.Pin}");
        var avatar = new Avatar
        {
            ImageName = argument.ImageName,
            Image = Convert.FromBase64String(argument.Image)
        };
        await _context.Avatars.AddAsync(avatar);
        await _context.SaveChangesAsync();
        var person = new Person
        {
            LastName = argument.LastName,
            FirstName = argument.FirstName,
            Gender = argument.Gender,
            PassportSeriesNumber = argument.PassportSeriesNumber,
            RegisteredAddress = argument.RegisteredAddress,
            ActualAddress = argument.ActualAddress,
            Pin = argument.Pin,
            BirthDate = argument.BirthDate,
            AvatarId = avatar.Id,
            DeathDate = argument.DeathDate,
        };
        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<PersonDto>(person));
    }
}

public class CreatePersonMemberArgument
{
    public string? Pin { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required Gender Gender { get; set; }
    
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? BirthPlace { get; set; }
    public string? PassportSeriesNumber { get; set; }
    public required string? RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
    public string Image { get; set; }
    public string ImageName { get; set; }
}