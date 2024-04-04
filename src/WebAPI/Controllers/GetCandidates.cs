using Application.Common.Dto;
using Application.Common.Extensions;
using AutoMapper.QueryableExtensions;
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
        var candidates = await _context.PersonCandidates.Include(x=>x.Person)
            .Where(x => x.Person.Pin != null && (argument.Pin == null || x.Person.Pin.Contains(argument.Pin)
                || argument.Fullname == null || (x.Person.LastName + ' ' + x.Person.FirstName + ' ' + x.Person.PatronymicName).Contains(argument.Fullname)))
            .ProjectTo<CandidateDto>(_mapper.ConfigurationProvider)
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