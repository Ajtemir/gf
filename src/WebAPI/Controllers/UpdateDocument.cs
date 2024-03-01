using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class DocumentsController
{
    [HttpPost("update")]
    public async Task<ActionResult> UpdateDocument([FromBody]UpdateDocumentArgument argument)
    {
        var document = await _context.Documents.Include(x=>x.DocumentType).FirstOrErrorAsync(x => x.Id == argument.DocumentId);
        if (argument.File is null || argument.FileName is null)
        {
            document.Reset();
        }
        else
        {
            document.Bytes = Convert.FromBase64String(argument.File.Split(',')[1]);
            document.Name = argument.FileName;
        }

        await _context.SaveChangesAsync();
        var response = _mapper.Map<DocumentDto>(document);
        return Ok(response);
    }
}

public class UpdateDocumentArgument
{
    public int DocumentId { get; set; }
    public string? File { get; set; }
    public string? FileName { get; set; }
}
