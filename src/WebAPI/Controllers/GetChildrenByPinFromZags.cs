using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;
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
        var childList = new List<Child>();
        foreach (var child in zagsChildrenResult.Children)
        {
            var existPerson = await _context.Persons.AsNoTracking().FirstOrDefaultAsync(x => x.Pin == child.Pin);
            if (existPerson != null)
            {
                var foundChild = await _context.Children.FirstOrDefaultAsync(x => x.PersonId == existPerson.Id);
                if (foundChild != null)
                {
                    childList.Add(foundChild);
                    continue;
                }

                var createdChild = new Child { PersonId = existPerson.Id, };
                await _context.Children.AddAsync(createdChild);
                await _context.SaveChangesAsync();
                childList.Add(createdChild);
                continue;
            }
            var pin = new ArgumentPin
            {
                Pin = long.Parse(child.Pin)
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
                var createdChild = new Child
                {
                    PersonId = person.Id,
                };
                await _context.Children.AddAsync(createdChild);
                await _context.SaveChangesAsync();
                await _context.MotherChildren.AddAsync(new MotherChild
                {
                    MotherId = mother.Id,
                    ChildId = createdChild.Id,
                });
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                childList.Add(createdChild);
                
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new BadRequestException(e.Message);
            }
        }
        
        await _context.MotherChildren.AddRangeAsync(childList.Select(x=>new MotherChild
        {
            ChildId = x.Id,
            MotherId = mother.Id,
        }));
        await _context.SaveChangesAsync();
        var result = await _context.MotherChildren
            .Include(x => x.Child).Where(x => x.MotherId == mother.Id)
            .Select(x=>x.Child).ToListAsync();
        return Ok(result);
    }

    public class GetChildrenByPinFromZagsArgument
    {
        public int MotherId { get; set; }
    }
}