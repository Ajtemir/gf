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

public class GetPositionQuery : IRequest<PositionDto>
{
    public int Id { get; set; }
    
    public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPositionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(GetPositionQuery request, CancellationToken cancellationToken)
        {
            var positionDto = await _context.Positions.AsNoTracking()
                .ProjectTo<PositionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (positionDto is null)
            {
                throw new NotFoundException(nameof(Position), request.Id);
            }

            return positionDto;
        }
    }

    public class GetPositionQueryValidator : AbstractValidator<GetPositionQuery>
    {
        public GetPositionQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}