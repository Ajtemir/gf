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

namespace Application.Citizens.Queries;

public class GetCitizenQuery : IRequest<CitizenDto>
{
    public int Id { get; set; }
    
    public class GetCitizenQueryHandler : IRequestHandler<GetCitizenQuery, CitizenDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitizenQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitizenDto> Handle(GetCitizenQuery request, CancellationToken cancellationToken)
        {
            var citizenDto = await _context.Citizens.AsNoTracking()
                .Where(x => x.Id == request.Id)
                .ProjectTo<CitizenDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (citizenDto is null)
            {
                throw new NotFoundException(nameof(Citizen), request.Id);
            }

            return citizenDto;
        }
    }


    public class GetCitizenQueryValidator : AbstractValidator<GetCitizenQuery>
    {
        public GetCitizenQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}