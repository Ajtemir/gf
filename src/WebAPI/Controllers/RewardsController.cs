using Application.Common.Dto;
using Application.Rewards.Commands;
using Application.Rewards.Queries;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public partial class RewardsController : ApiControllerBase
{
    private readonly ApplicationDbContext _context;

    public RewardsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RewardDto>>> GetRewards(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetRewardListQuery(), cancellationToken));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RewardDto>> GetReward(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetRewardQuery { Id = id }, cancellationToken);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<RewardDto>> Create(CreateRewardCommand request, CancellationToken cancellationToken)
    {
        var rewardDto = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction("GetReward", new { id = rewardDto.Id }, rewardDto);
    }

    [HttpPut("{id:int}/update-details")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateRewardDetailsCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id:int}/update-image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateRewardImageCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}/delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteRewardCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}