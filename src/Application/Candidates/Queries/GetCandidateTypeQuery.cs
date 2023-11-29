using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Candidates.Queries;

public class GetCandidateTypeQuery : IRequest<string>
{
    public required int Id { get; set; }
    
    public class GetCandidateTypeQueryHandler : IRequestHandler<GetCandidateTypeQuery, string>
    {
        private readonly IApplicationDbContext _context;

        public GetCandidateTypeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetCandidateTypeQuery request, CancellationToken cancellationToken)
        {
            var candidateType = await _context.Candidates.Where(x => x.Id == request.Id)
                .Select(x => x.CandidateType)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (candidateType is null)
            {
                throw new NotFoundException(nameof(Candidate), request.Id);
            }
        
            return candidateType;
        }
    }

    public class GetCandidateTypeQueryValidator : AbstractValidator<GetCandidateTypeQuery>
    {
        public GetCandidateTypeQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }

}