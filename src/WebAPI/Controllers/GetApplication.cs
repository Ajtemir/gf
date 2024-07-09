using System.Text.Json.Serialization;
using Application.Common.Dto;
using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;

namespace WebAPI.Controllers;

public partial class ApplicationsController
{
    // [HttpGet("{applicationId:int}")]
    // public async Task<ActionResult> GetApplication(int applicationId)
    // {
    //     var application = await _context.RewardApplications
    //         .AsNoTracking()
    //         .Include(x=>x.Reward)
    //         .Include(x=>x.Candidate)
    //         .Include(x => x.RewardApplicationStatuses).ThenInclude(x=> x.Office)
    //         .Include(x => x.RewardApplicationStatuses).ThenInclude(x=> x.User)
    //         .Include(x => x.ApplicationDocuments)
    //         .ThenInclude(x=> x.Document)
    //         .ThenInclude(x=> x.DocumentType)
    //         .FirstOrErrorAsync(x => x.Id == applicationId, $"Application by id({applicationId}) not found.");
    //     var result = _mapper.Map<ApplicationDto>(application);
    //     return Ok(result);
    // }

    public class GetApplicationResult
    {
        public int CandidateId { get; set; }
        [JsonIgnore]
        public Candidate Candidate { get; set; }
        public int Id { get; set; }
        public List<StatusDto> Statuses { get; set; }
        public List<DocumentDto> Documents { get; set; }
        public int RewardId { get; set; }
        // [JsonIgnore]
        public Reward Reward { get; set; }
    }

    // public class GetApplicationResultProfile : Profile
    // {
    //     public GetApplicationResultProfile()
    //     {
    //         CreateMap<Domain.Entities.Application, GetApplicationResult>()
    //             .ForMember(d => d.Id, x => x.MapFrom(s => s.Id))
    //             .ForMember(d => d.Documents, x => x.MapFrom(s => s.ApplicationDocuments.Select(d=>d.Document)))
    //             .ForMember(d => d.Statuses, x => x.MapFrom(s => s.RewardApplicationStatuses))
    //         ;
    //     }
    // }
}