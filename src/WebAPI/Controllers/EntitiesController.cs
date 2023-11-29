using Application.Candidates.Commands;
using Application.Common.Dto;
using Application.Entities.Commands;
using Application.Entities.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class EntitiesController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EntityDto>>> GetEntities(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetEntitiesListQuery(), cancellationToken));
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EntityDto>> GetEntity(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetEntityQuery { Id = id }, cancellationToken);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<EntityDto>> Create(CreateEntityCommand request, CancellationToken cancellationToken)
    {
        var entityDto = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction("GetEntity", new { id = entityDto.Id }, entityDto);
    }
    
    [HttpPut("{id:int}/update-details")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDetails(int id, UpdateEntityCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id:int}/update-image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateImage(int id, UpdateEntityImageCommand request, CancellationToken cancellationToken)
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
        await Mediator.Send(new DeleteCandidateCommand() { Id = id }, cancellationToken);
        return NoContent();
    }
}