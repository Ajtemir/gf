using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Candidates.Queries;

public class GetCandidateQuery : IRequest<CandidateDto>
{
    public int Id { get; set; }
    
    public class GetCandidateQueryHandler : IRequestHandler<GetCandidateQuery, CandidateDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCandidateQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CandidateDto> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            var candidateDto = await _context.Candidates
                .Where(x => x.Id == request.Id)
                .ProjectTo<CandidateDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (candidateDto is null)
            {
                throw new NotFoundException(nameof(Candidate), request.Id);
            }

            return candidateDto;
        }
    }

    public class GetCandidateQueryValidator : AbstractValidator<GetCandidateQuery>
    {
        public GetCandidateQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}