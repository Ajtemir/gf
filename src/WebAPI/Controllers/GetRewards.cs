using Application.Common.Dto;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

public partial class RewardsController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetRewards()
    {
        var rewards = await _context.Rewards.ProjectTo<RewardDto>(_mapper.ConfigurationProvider).ToListAsync();
        return Ok(rewards);
    }
}