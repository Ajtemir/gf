using Application.Common.Dto;
using Application.Common.Extensions;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class ChildrenController
{
    [HttpGet("[action]")]
    public async Task<ActionResult> GetChild([FromQuery]GetChildArgument argument)
    {
        var child = await _context.Children
            .Include(x=>x.Person)
            .Include(x=>x.MotherChild)
            .ProjectTo<ChildInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrErrorAsync(x => x.Id == argument.ChildId);
        return Ok(child);
    }

    public class GetChildArgument
    {
        public int  ChildId { get; set; }
    }
}