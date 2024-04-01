using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Integrator;

namespace WebAPI.Controllers;

public partial class ChildrenController
{
    [HttpGet("[action]")]
    public async Task<ActionResult> GetChildrenByPinFromZags([FromQuery]GetChildrenByPinFromZagsArgument argument)
    {
        var zagsChildrenResult = await new GetZagsChildren(new ArgumentPin { Pin = argument.Pin }).ExecuteAsync();
        await Parallel.ForEachAsync(zagsChildrenResult.Children, async (child, token) =>
        {
            var pin = new ArgumentPin
            {
                Pin = argument.Pin
            };
            var passportInfo = await new GetPassportInfoByPin(pin).ExecuteAsync();
            var addressByPin = await new GetAddressByPin(pin).ExecuteAsync();
            var zagsInfo = await new GetZagsByPin(pin).ExecuteAsync();
            var photoList = await new GetPhotoByPin(pin).ExecuteAsync();
            var lastPhoto = photoList.Photos.LastOrDefault()?.PersonImage;
            var passportSeriesNumber = passportInfo.PassportSeries?.ToUpper() + passportInfo.PassportNumber;
            var avatar = lastPhoto == null ? null : new Avatar
            {
                ImageName = $"{pin.Pin}.jpeg",
                Image = Convert.FromBase64String(lastPhoto)
            };
            var person = new Person
            {
                LastName = zagsInfo.Surname,
                FirstName = zagsInfo.Name,
                Gender = zagsInfo.Gender == 1 ? Gender.Male : Gender.Female,
                PassportSeriesNumber = passportSeriesNumber,
                RegisteredAddress = addressByPin.Data.CurrentAddress.Address,
                ActualAddress = null,
                Pin = argument.Pin.ToString(),
                BirthDate = DateOnly.FromDateTime(zagsInfo.Birthdate.Date),
                Avatar = avatar
            };
            return person;
        });
        
        return Ok(zagsChildrenResult);
    }

    public class GetChildrenByPinFromZagsArgument
    {
        public long Pin { get; set; }
    }
}