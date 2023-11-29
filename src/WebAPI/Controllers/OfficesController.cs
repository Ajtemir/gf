using Application.Common.Dto;
using Application.Offices.Commands;
using Application.Offices.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class OfficesController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OfficeDto>>> GetOffices(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetOfficeListQuery(), cancellationToken));
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OfficeDto>> GetOffice(int id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetOfficeQuery() { Id = id, IncludeParentOffices = true }, cancellationToken);
    }
    
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<OfficeDto>> Create(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var officeDto = await Mediator.Send(request, cancellationToken);
        return CreatedAtAction("GetOffice", new { id = officeDto.Id }, officeDto );
    }

    [HttpPut("{id:int}/update-details")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateOfficeCommand request, CancellationToken cancellationToken)
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
        await Mediator.Send(new DeleteOfficeCommand() { Id = id }, cancellationToken);
        return NoContent();
    }

    [HttpPost("add-parent-office")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddParentOffice(AddParentOfficeCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("delete-parent-office")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteParentOffice(DeleteParentOfficeCommand request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpPost("add-child-office")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddChildOffice(AddChildOfficeCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("delete-child-office")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteChildOffice(DeleteChildOfficeCommand request, CancellationToken cancellationToken)
    {
        await Mediator.Send(request, cancellationToken);
        return NoContent();
    }
}
