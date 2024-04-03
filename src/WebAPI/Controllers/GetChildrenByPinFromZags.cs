using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;
using WebAPI.Services.Integrator;

namespace WebAPI.Controllers;

public partial class ChildrenController
{
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult> GetChildrenByMotherIdFromZagsThroughPin([FromQuery]GetChildrenByPinFromZagsArgument argument)
    {
        var mother = await _context.Mothers.Include(x=>x.Person)
            .FirstOrErrorAsync(x => x.Id == argument.MotherId, "Mother not found");
        if (mother.Person.Pin == null)
            throw new BadRequestException("Candidate has not a pin");
        var zagsChildrenResult = await new GetZagsChildren(new ArgumentPin { Pin = long.Parse(mother.Person.Pin) }).ExecuteAsync();
        foreach (var child in zagsChildrenResult.Children)
        {
            var existPerson = await _context.Persons.AsNoTracking().FirstOrDefaultAsync(x => x.Pin == child.Pin);
            if (existPerson != null)
            {
                var foundChild = await _context.Children.FirstOrDefaultAsync(x => x.PersonId == existPerson.Id);
                if (foundChild != null)
                {
                    var motherChild =
                        await _context.MotherChildren.FirstOrDefaultAsync(x =>
                            x.MotherId == mother.Id && x.ChildId == foundChild.Id);
                    if (motherChild == null)
                    {
                        await _context.MotherChildren.AddAsync(new MotherChild
                        {
                            MotherId = mother.Id, ChildId = foundChild.Id
                        });
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    var createdChild = new Child { PersonId = existPerson.Id, };
                    await _context.SaveChildWithDocumentsAsync(createdChild);
                    await _context.AddChildToMotherIfNotExistsAsync(createdChild, mother.Id);
                }
            }
            else
            {

                var pin = new ArgumentPin { Pin = long.Parse(child.Pin) };
                var passportInfo = await new GetPassportInfoByPin(pin).ExecuteAsync();
                var addressByPin = await new GetAddressByPin(pin).ExecuteAsync();
                var zagsInfo = await new GetZagsByPin(pin).ExecuteAsync();
                var photoList = await new GetPhotoByPin(pin).ExecuteAsync();
                var lastPhoto = photoList.Photos.LastOrDefault()?.PersonImage;
                var passportSeriesNumber = passportInfo.PassportSeries?.ToUpper() + passportInfo.PassportNumber;
                var avatar = lastPhoto == null
                    ? null
                    : new Avatar { ImageName = $"{pin.Pin}.jpeg", Image = Convert.FromBase64String(lastPhoto) };
                var person = new Person
                {
                    LastName = zagsInfo.Surname,
                    FirstName = zagsInfo.Name,
                    Gender = zagsInfo.Gender == 1 ? Gender.Male : Gender.Female,
                    PassportSeriesNumber = passportSeriesNumber,
                    RegisteredAddress = addressByPin.Data.CurrentAddress.Address,
                    ActualAddress = null,
                    Pin = child.Pin,
                    BirthDate = DateOnly.FromDateTime(zagsInfo.Birthdate.Date),
                    Avatar = avatar
                };
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (avatar != null)
                    {
                        await _context.Avatars.AddAsync(avatar);
                        await _context.SaveChangesAsync();
                        person.AvatarId = avatar.Id;
                    }

                    await _context.Persons.AddAsync(person);
                    await _context.SaveChangesAsync();
                    var createdChild = new Child { PersonId = person.Id, };
                    await _context.SaveChildWithDocumentsAsync(createdChild);
                    await _context.AddChildToMotherIfNotExistsAsync(createdChild, mother.Id);
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new BadRequestException(e.Message);
                }
            }
        }
        return Ok();
    }

    public class GetChildrenByPinFromZagsArgument
    {
        public int MotherId { get; set; }
    }
}