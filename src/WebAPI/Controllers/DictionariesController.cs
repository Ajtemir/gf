using Application.Common.Dto;
using Application.Dictionaries.Commands;
using Application.Dictionaries.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class DictionariesController : ApiControllerBase
{
    #region Position
    
    [HttpGet("positions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetPositionListQuery(), cancellationToken));
    }

    [HttpGet("positions/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PositionDto>> GetPosition(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetPositionQuery { Id = id }, cancellationToken);
    }

    [HttpPost("positions/create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<PositionDto>> CreatePosition(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        var positionDto = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction("GetPosition", new { id = positionDto.Id }, positionDto);
    }

    [HttpPut("positions/update/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdatePosition(int id, UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("positions/delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePosition(int id, UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    #endregion Position
    
    #region Education
    
    [HttpGet("educations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EducationDto>>> GetEducations(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetEducationListQuery(), cancellationToken));
    }
    
    [HttpGet("educations/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<EducationDto>> GetEducation(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetEducationQuery { Id = id }, cancellationToken);
    }
    
    #endregion Education
    
    #region Citizenship
    
    [HttpGet("citizenships")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CitizenshipDto>>> GetCitizenships(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetCitizenshipListQuery(), cancellationToken));
    }
    
    [HttpGet("citizenships/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CitizenshipDto>> GetCitizenship(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCitizenshipQuery { Id = id }, cancellationToken);
    }
    
    #endregion Citizenship
}