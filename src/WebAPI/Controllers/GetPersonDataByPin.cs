using Application.Common.Exceptions;
using Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;
using WebAPI.Services.Integrator;

namespace WebAPI.Controllers;

public partial class MembersController
{
    [HttpGet]
    public async Task<ActionResult> GetPersonDataByPin([FromQuery] GetPersonDataByPinArgument argument)
    {
        var pin = new ArgumentPin { Pin = argument.Pin };
        var passportInfo = await new GetPassportInfoByPin(pin).ExecuteAsync();
        var addressByPin = await new GetAddressByPin(pin).ExecuteAsync();
        var zagsInfo = await new GetZagsByPin(pin).ExecuteAsync();
        var photoList = await new GetPhotoByPin(pin).ExecuteAsync();
        var passportSeriesNumber = passportInfo.PassportSeries?.ToUpper() + passportInfo.PassportNumber;
        GetPersonDataByPinResult result = new()
        {
            LastName = zagsInfo.Surname,
            FirstName = zagsInfo.Name,
            PatronymicName = zagsInfo.Patronymic,
            RegisteredAddress = addressByPin.Data.CurrentAddress.Address,
            Gender = zagsInfo.Gender == 1 ? Gender.Male : Gender.Female,
            Pin = zagsInfo.Pin.ToString(),
            BirthDate = DateOnly.FromDateTime(zagsInfo.Birthdate.Date),
            DeathDate = zagsInfo.DeathDate?.Date == null ? null : DateOnly.FromDateTime(zagsInfo.DeathDate.Date),
            PassportSeriesNumber = passportSeriesNumber,
            Image = photoList.Photos.LastOrDefault()?.PersonImage,
        };
        return Ok(result);
    }
}

public class GetPersonDataByPinArgument
{
    public long Pin { get; set; }
}

public class CreateApplicationArgumentValidator : AbstractValidator<GetPersonDataByPinArgument>
{
    public CreateApplicationArgumentValidator()
    {
        RuleFor(x => x.Pin).NotEmpty();
    }
}

public class GetPersonDataByPinResult
{
    public string? Pin { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? PassportSeriesNumber { get; set; }
    public required string? RegisteredAddress { get; set; }
    public string? Image { get; set; }
}