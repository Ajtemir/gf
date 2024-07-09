using Application.Common.Dto;
using Application.Common.Extensions;
using Application.Common.MappingProfiles;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

public partial class CandidatesController
{
    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<ActionResult> GetCandidates([FromQuery]GetCandidatesArgument argument)
    {
        var candidates = await _context.Candidates
            .Include(x=>((PersonCandidate)x).Person)
            .Include(x=>((Entity)x).PinEntity)
            .Where(x => 
                (
                    (argument.Pin == null || ((PersonCandidate)x).Person.Pin != null && ((PersonCandidate)x).Person.Pin!.Contains(argument.Pin))
                    ||
                    (argument.Pin == null || ((Entity)x).PinEntity.Pin != null && ((Entity)x).PinEntity.Pin!.Contains(argument.Pin))
                )
                && 
                (
                    (argument.Fullname == null || (((PersonCandidate)x).Person.LastName + ' ' + ((PersonCandidate)x).Person.FirstName + ' ' + ((PersonCandidate)x).Person.PatronymicName).Contains(argument.Fullname))
                    ||
                    (argument.Fullname == null || ((Entity)x).NameKg.Contains(argument.Fullname) || ((Entity)x).NameRu.Contains(argument.Fullname))
                )
            )
            .ProjectTo<CandidateListItemDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(argument.PageNumber, argument.PageSize);
        return Ok(candidates);
    }
    
    public class GetCandidatesArgument
    {
        public string? Pin { get; set; }
        public string? Fullname { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}