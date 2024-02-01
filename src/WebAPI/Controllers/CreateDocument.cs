using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class DocumentsController
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody]CreateDocumentArgument argument)
    {
        return Ok();
        // var application = await _context.RewardApplications
        //     .Include(x=>x.Documents)
        //     .Include(x => x.Candidate)
        //     .ThenInclude(x=> x.CandidateType)
        //     .ThenInclude(x => x.CandidateTypesDocumentTypes)
        //     .FirstOrErrorAsync(x => x.Id == argument.ApplicationId, "Application by id not found");
        //
        // var documentTypesIds = application.Candidate!.CandidateType.CandidateTypesDocumentTypes.Select(x => x.DocumentTypeId).ToList();
        //
        // if (documentTypesIds.NotContains(argument.DocumentTypeId))
        // {
        //     throw new NotFoundException("Applicant should not have such a document type");
        // }
        //
        // if (application.Documents.Select(x => x.DocumentTypeId).Contains(argument.DocumentTypeId))
        // {
        //     throw new BadRequestException("This document type has already uploaded, so please replace it");
        // }
        //
        // var ext = Path.GetExtension(argument.DocumentName).Remove('.');
        // var extensions = new[] { "pdf", "jpg", "jpeg" };
        // if (extensions.NotContains(ext))
        // {
        //     throw new BadRequestException($"This extension is not valid. Valid extensions: {string.Join(" ,", extensions)}");
        // }
        // var document = new Document
        // {
        //     DocumentTypeId = argument.DocumentTypeId,
        //     Bytes = Convert.FromBase64String(argument.Document),
        //     Extension = ext,
        //     Name = argument.DocumentName,
        //     RewardApplicationId = argument.ApplicationId,
        //     ContentType = $"application/{ext}",  
        // };
        // _context.Documents.Add(document);
        // await _context.SaveChangesAsync();
        // return Ok(document.Id);
    }

    public class CreateDocumentArgument
    {
        public int ApplicationId { get; set; }
        public int DocumentTypeId { get; set; }
        public required string Document { get; set; }
        public required string DocumentName { get; set; }
    }
}