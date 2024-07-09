using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class CandidatesController
{
    [HttpGet("[action]/{personCandidateId:int}")]
    public async Task<ActionResult> GetCandidateSpecificData(int personCandidateId)
    {
        var candidateType = await _context.Candidates.Where(x=>x.Id == personCandidateId).Select(x=>x.CandidateTypeId).FirstOrErrorAsync();
        return candidateType switch
        {
            CandidateTypes.Citizen => Ok(await _context.Citizens.Select(x => new
                {
                    x.Id,
                    x.Education,
                    x.EducationId,
                    x.ScienceDegree,
                    x.YearsOfWorkTotal,
                    x.YearsOfWorkInCollective,
                    x.YearsOfWorkInIndustry,
                    CandidateType = x.CandidateTypeId,
                })
                .FirstOrDefaultAsync(x => x.Id == personCandidateId)),
            CandidateTypes.Mother => Ok(await _context.Citizens.Select(x => new { CandidateType = x.CandidateTypeId, x.Id })
                .FirstOrDefaultAsync(x => x.Id == personCandidateId)),
            CandidateTypes.Foreigner => Ok(await _context.Foreigners.Include(x=>x.Citizenship)
                .Select(x => new { x.Id, x.Citizenship, x.CitizenshipId, CandidateType = x.CandidateTypeId, CitizenshipNameKg = x.Citizenship!.NameKg, CitizenshipNameRu = x.Citizenship.NameRu })
                .FirstOrErrorAsync(x => x.Id == personCandidateId)),
            CandidateTypes.Entity => Ok(await _context.Entities
                .Select(x => new { x.Id, x.NameKg, x.NameRu, CandidateType = x.CandidateTypeId, })
                .FirstOrErrorAsync(x => x.Id == personCandidateId)),
            _ => throw new BadRequestException("Not Found")
        };
    }
}
