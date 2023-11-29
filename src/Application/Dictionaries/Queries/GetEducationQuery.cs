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

public class GetEducationQuery : IRequest<EducationDto>
{
    public int Id { get; set; }
    
    public class GetEducationQueryHandler : IRequestHandler<GetEducationQuery, EducationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEducationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EducationDto> Handle(GetEducationQuery request, CancellationToken cancellationToken)
        {
            var educationDto = await _context.Educations.AsNoTracking()
                .ProjectTo<EducationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (educationDto is null)
            {
                throw new NotFoundException(nameof(Education), request.Id);
            }
            
            return educationDto;
        }
    }

    public class GetEducationQueryValidator : AbstractValidator<GetEducationQuery>
    {
        public GetEducationQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}