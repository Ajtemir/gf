using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Dictionary;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Queries;

public class GetCitizenshipQuery : IRequest<CitizenshipDto>
{
    public int Id { get; set; }
    
    public class GetCitizenshipQueryHandler : IRequestHandler<GetCitizenshipQuery, CitizenshipDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitizenshipQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitizenshipDto> Handle(GetCitizenshipQuery request, CancellationToken cancellationToken)
        {
            var citizenshipDto = await _context.Citizenships.AsNoTracking()
                .ProjectTo<CitizenshipDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (citizenshipDto is null)
            {
                throw new NotFoundException(nameof(Education), request.Id);
            }
            
            return citizenshipDto;
        }
    }

    public class GetCitizenshipQueryValidator : AbstractValidator<GetCitizenshipQuery>
    {
        public GetCitizenshipQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}