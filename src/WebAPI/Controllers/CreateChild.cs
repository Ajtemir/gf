using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class ChildrenController
{
    [HttpPost("[action]")]
    public async Task<ActionResult> Create([FromBody]CreateChildArgument argument)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Pin == argument.Pin);
            if (member == null)
            {
                member = new Member { Pin = argument.Pin };
                await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
            }

            var child = new Child
            {
                FirstName = argument.FirstName,
                LastName = argument.LastName,
                PatronymicName = argument.PatronymicName,
                Gender = argument.Gender,
                IsAdopted = argument.IsAdopted,
                MemberId = member.Id
            };
            await _context.Children.AddAsync(child);
            await _context.SaveChangesAsync();
            var documentTypes = await _context.ChildDocumentTypes.ToListAsync();
            var addedDocuments = documentTypes.Select(x => new Document { DocumentTypeId = x.DocumentTypeId }).ToList();
            await _context.Documents.AddRangeAsync(addedDocuments);
            await _context.SaveChangesAsync();
            await _context.ChildDocuments.AddRangeAsync(addedDocuments.Select(x => new ChildDocument
            {
                DocumentId = x.Id, ChildId = child.Id
            }));
            await _context.SaveChangesAsync();
            return Ok(child.Id);
        }
        catch(Exception exception)
        {
            await transaction.RollbackAsync();
            throw new BadRequestException();
        }
    }
}

public class CreateChildArgument : IFullName, IPin, IGender
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PatronymicName { get; set; }
    public string Pin { get; set; }
    public GenderType Gender { get; set; }
    
    public bool  IsAdopted { get; set; }
    public int Mother { get; set; }
}

public interface IFullName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PatronymicName { get; set; }
}

public interface IPin
{
    public string Pin { get; set; }
}

public interface IGender
{
    public GenderType Gender { get; set; }
}