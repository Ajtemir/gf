using Application.Candidates.Commands;
using Application.Citizens.Commands;
using Application.Citizens.Queries;
using Application.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class CitizensController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CitizenDto>>> GetCitizens(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetCitizensListQuery(), cancellationToken));
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CitizenDto>> GetCitizen(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCitizenQuery { Id = id }, cancellationToken);
    }
    
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CitizenDto>> Create(CreateCitizenCommand request, CancellationToken cancellationToken)
    {
        var citizenDto = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction("GetCitizen", new { id = citizenDto.Id }, citizenDto);
    }
    
    [HttpPut("{id:int}/update-details")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDetails(int id, UpdateCitizenCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id:int}/update-image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateImage(int id, UpdateCitizenImageCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteCandidateCommand() { Id = id }, cancellationToken);
        return NoContent();
    }
}