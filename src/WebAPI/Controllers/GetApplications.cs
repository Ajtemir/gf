﻿using Application.Common.Dto;
using Application.Common.Extensions;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

public partial class ApplicationsController
{
    [HttpGet]
    public async Task<ActionResult> GetApplications(int pageSize = 10, int pageNumber = 1)
    {
        var applications = await _context.RewardApplications
            .Include(x=>x.Reward)
            .Include(x=>x.Candidate)
            .ProjectTo<ApplicationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(pageNumber, pageSize);
        return Ok(applications);
    }
    
    
}



