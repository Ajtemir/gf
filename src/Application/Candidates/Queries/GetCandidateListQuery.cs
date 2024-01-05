using Application.Common.Dto;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Candidates.Queries;

public class GetCandidateListQuery : IRequest<IEnumerable<CandidateWithoutImageDto>>
{
    public class GetCandidateListQueryHandler : IRequestHandler<GetCandidateListQuery, IEnumerable<CandidateWithoutImageDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCandidateListQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CandidateWithoutImageDto>> Handle(GetCandidateListQuery request, CancellationToken cancellationToken)
        {
            var query = from candidate in _context.Candidates
                select new CandidateWithoutImageDto()
                {
                    Id = candidate.Id,
                    CandidateType = candidate.CandidateTypeId,
                    Name = candidate is Entity ? ((Entity)candidate).NameRu :
                           candidate is Person ? string.Join(' ', ((Person)candidate).LastName, ((Person)candidate).FirstName, ((Person)candidate).PatronymicName) :
                           "",
                    CreatedBy = candidate.CreatedBy,
                    CreatedByUser = candidate.CreatedByUser!.UserName,
                    CreatedAt = candidate.CreatedAt,
                    ModifiedBy = candidate.ModifiedBy,
                    ModifiedByUser = candidate.ModifiedByUser!.UserName,
                    ModifiedAt = candidate.ModifiedAt,
                };

            var candidateDtos = await query.ToListAsync(cancellationToken);
            return candidateDtos;
        }
    }
}