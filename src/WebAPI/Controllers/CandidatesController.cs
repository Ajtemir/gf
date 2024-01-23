using System.ComponentModel;
using Application.Candidates.Commands;
using Application.Candidates.Queries;
using Application.Citizens.Queries;
using Application.Common.Dto;
using Application.Entities.Queries;
using Application.Foreigners.Queries;
using Application.Mothers.Queries;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace WebAPI.Controllers;

[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class CandidatesController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CandidateDto>>> GetCandidates(CancellationToken cancellationToken)
    {
        var candidateDtos = await Mediator.Send(new GetCandidateListQuery(), cancellationToken);
        return Ok(candidateDtos);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CandidateDto>> GetCandidate(int id, CancellationToken cancellationToken)
    {
        var candidateType = await Mediator.Send(new GetCandidateTypeQuery { Id = id }, cancellationToken);
        CandidateDto candidateDto = candidateType switch
        {
            CandidateTypes.Citizen => await Mediator.Send(new GetCitizenQuery { Id = id }, cancellationToken),
            CandidateTypes.Mother => await Mediator.Send(new GetMotherQuery { Id = id }, cancellationToken),
            nameof(Foreigner) => await Mediator.Send(new GetForeignerQuery { Id = id }, cancellationToken),
            nameof(Entity) => await Mediator.Send(new GetEntityQuery { Id = id }, cancellationToken),
            _ => throw new InvalidEnumArgumentException($"Invalid {nameof(Candidate.CandidateTypeId)}: {candidateType}")
        };

        return Ok(candidateDto);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCandidate(int id, CancellationToken cancellationToken)
    {
        var deleteCandidateCommand = new DeleteCandidateCommand() { Id = id };
        await Mediator.Send(deleteCandidateCommand, cancellationToken);
        return NoContent();
    }
}