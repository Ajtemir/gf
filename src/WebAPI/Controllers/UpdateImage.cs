using Application.Common.Dto;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class ImagesController
{
    [HttpPost]
    public async Task<ActionResult> UpdateImage([FromBody]AvatarDto argument)
    {
        var avatar = await _context.Avatars.FirstOrErrorAsync(x=>x.Id == argument.Id);
        avatar.Image = argument.Image == null ? null : Convert.FromBase64String(argument.Image);
        avatar.ImageName = argument.ImageName;
        await _context.SaveChangesAsync();
        var result = _mapper.Map<AvatarDto>(avatar);
        return Ok(result);
    }
}