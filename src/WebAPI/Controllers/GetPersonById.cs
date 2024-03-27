using Application.Common.Dto;
using Application.Common.Extensions;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class MembersController
{
    [HttpGet("{memberId:int}")]
    public async Task<ActionResult> GetPersonById(int memberId)
    {
        var person = await _context.Persons
            .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
            .FirstOrErrorAsync(x=>x.Id == memberId, "Person not found");
        return Ok(person);
    }
}