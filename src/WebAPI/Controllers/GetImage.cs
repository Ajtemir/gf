using Application.Common.Dto;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class ImagesController
{
    [HttpGet("{imageId:int}")]
    public async Task<ActionResult> GetImage(int imageId)
    {
        var image = await _context.Avatars.ProjectTo<AvatarDto>(_mapper.ConfigurationProvider).FirstOrErrorAsync(x => x.Id == imageId);
        return Ok(image);
    }
}
