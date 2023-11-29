using Application.Candidates.Commands;
using Application.Common.Dto;
using Application.Mothers.Commands;
using Application.Mothers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class MothersController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MotherDto>>> GetMothers(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetMotherListQuery(), cancellationToken));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MotherDto>> GetMother(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetMotherQuery { Id = id }, cancellationToken);
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<MotherDto>> Create(CreateMotherCommand request, CancellationToken cancellationToken)
    {
        var motherDto = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction("GetMother", new { id = motherDto.Id }, motherDto);
    }

    [HttpPut("{id:int}/update-details")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDetails(int id, UpdateMotherCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id:int}/update-image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateImage(int id, UpdateMotherImageCommand request, CancellationToken cancellationToken)
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