using Application.Common.Dto;
using Application.Common.Exceptions;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

public partial class CandidatesController
{
    [HttpGet("[action]/{personId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetCandidatesByPersonId(int personId)
    {
        var candidates = await _context.PersonCandidates.Where(x => x.PersonId == personId)
            .ProjectTo<CandidateDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(candidates);
    }
}