using Application.Common.Dto;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

public partial class ChildrenController
{
    [HttpGet("[action]")]
    public async Task<ActionResult> GetChildrenByMotherId([FromQuery]GetChildrenMyMotherIdArgument argument)
    {
        var children = await _context.MotherChildren.Include(x => x.Child)
            .Where(x => x.MotherId == argument.MotherId)
            .Select(x=>x.Child)
            .ToListAsync();
        return Ok(children);
    }

    public class GetChildrenMyMotherIdArgument
    {
        public int  MotherId { get; set; }
    }
}