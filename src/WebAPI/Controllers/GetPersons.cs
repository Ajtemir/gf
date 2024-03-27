using Application.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

public partial class MembersController
{
    [HttpGet]
    public async Task<ActionResult> GetMembers(int pageSize = 10, int pageNumber = 1)
    {
        var applications = await _context.Members
            .ToPaginatedListAsync(pageNumber, pageSize);
        return Ok(applications);
    }
}


