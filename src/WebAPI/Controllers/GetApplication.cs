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
            .Include(x => x.RewardApplicationStatuses)
            .Include(x => x.Documents)
            .FirstOrErrorAsync(x => x.Id == applicationId, $"Application by id({applicationId}) not found.");
        return Ok(application);

    }

    public class GetApplicationResult
    {
        
    }
}