using Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;


public partial class MembersController
{
    [HttpPost]
    public async Task<ActionResult> UpdatePerson([FromBody]UpdatePersonArgument argument)
    {
        var person = await _context.Persons.FirstOrErrorAsync(x => x.Id == argument.Id);
        person.Gender = argument.Gender;
        person.FirstName = argument.FirstName;
        person.LastName = argument.LastName;
        person.PatronymicName = argument.PatronymicName;
        person.ActualAddress = argument.ActualAddress;
        person.RegisteredAddress = argument.RegisteredAddress;
        person.PassportSeriesNumber = argument.PassportSeriesNumber;
        person.BirthDate = argument.BirthDate;
        await _context.SaveChangesAsync();
        return Ok(person);
    }
}
public class UpdatePersonArgument : PersonDto
{
    
}