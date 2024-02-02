using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class ApplicationsController
{
    [HttpGet("{applicationId:int}")]
    public async Task<ActionResult> GetApplication(int applicationId)
    {
        var application = await _context.RewardApplications
            .AsNoTracking()
            .Include(x => x.RewardApplicationStatuses)
            .Include(x => x.Documents)
            .FirstOrErrorAsync(x => x.Id == applicationId, $"Application by id({applicationId}) not found.");
        return Ok(application);
    }

    public class GetApplicationResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RewardApplicationStatus> Statuses { get; set; }
        public List<Document> Documents { get; set; }
    }

    public class DocumentViewModel
    {
        public int? Id { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}