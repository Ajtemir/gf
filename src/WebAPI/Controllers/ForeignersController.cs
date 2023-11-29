using Application.Candidates.Commands;
using Application.Common.Dto;
using Application.Foreigners.Commands;
using Application.Foreigners.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class ForeignersController : ApiControllerBase
{
      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async Task<ActionResult<IEnumerable<ForeignerDto>>> GetForeigners(CancellationToken cancellationToken)
      {
          return Ok(await Mediator.Send(new GetForeignerListQuery(), cancellationToken));
      }
  
      [HttpGet("{id:int}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<ActionResult<ForeignerDto>> GetForeigner(int id, CancellationToken cancellationToken)
      {
          return await Mediator.Send(new GetForeignerQuery { Id = id }, cancellationToken);
      }
  
      [HttpPost("create")]
      [ProducesResponseType(StatusCodes.Status201Created)]
      public async Task<ActionResult<ForeignerDto>> Create(CreateForeignerCommand request, CancellationToken cancellationToken)
      {
          var foreignerDto = await Mediator.Send(request, cancellationToken);
          return CreatedAtAction("GetForeigner", new { id = foreignerDto.Id }, foreignerDto);
      }
  
      [HttpPut("{id:int}/update-details")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> UpdateDetails(int id, UpdateForeignerCommand request, CancellationToken cancellationToken)
      {
          request.Id = id;
          await Mediator.Send(request, cancellationToken);
          return NoContent();
      }
      
      [HttpPut("{id:int}/update-image")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> UpdateImage(int id, UpdateForeignerImageCommand request, CancellationToken cancellationToken)
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