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

namespace Application.Foreigners.Queries;

public class GetForeignerQuery : IRequest<ForeignerDto>
{
    public int Id { get; set; }

    public class GetForeignerQueryHandler : IRequestHandler<GetForeignerQuery, ForeignerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetForeignerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ForeignerDto> Handle(GetForeignerQuery request, CancellationToken cancellationToken)
        {
            var foreignerDto = await _context.Foreigners
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .ProjectTo<ForeignerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (foreignerDto is null)
            {
                throw new NotFoundException(nameof(Foreigner), request.Id);
            }

            return foreignerDto;
        }
    }

    public class GetForeignerQueryValidator : AbstractValidator<GetForeignerQuery>
    {
        public GetForeignerQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}