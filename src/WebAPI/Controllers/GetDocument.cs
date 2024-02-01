using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class DocumentsController
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetDocument(int id)
    {
        Document document = await _context.Documents.FirstOrErrorAsync(x => x.Id == id, $"File by id({id}) not found.");
        return new FileContentResult(document.Bytes, contentType: document.ContentType);
    }
}