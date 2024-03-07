using Application.Common.Dto;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class DocumentsController
{
    [HttpGet("[action]/{childId:int}")]
    public async Task<ActionResult> GetDocumentsByChildId(int childId)
    {
        var documents = await _context.ChildDocuments
            .Include(x=> x.Document)
            .ThenInclude(x=> x.DocumentType)
            .Where(x=> x.ChildId == childId)
            .Select(x=> x.Document)
            .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(documents);
    }
}