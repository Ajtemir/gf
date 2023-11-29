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

namespace Application.Entities.Queries;

public class GetEntityQuery : IRequest<EntityDto>
{
    public int Id { get; set; }

    public class GetEntityQueryHandler : IRequestHandler<GetEntityQuery, EntityDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEntityQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EntityDto> Handle(GetEntityQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Entities.AsNoTracking()
                .Where(x => x.Id == request.Id)
                .ProjectTo<EntityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Entity), request.Id);
            }

            return entity;
        }
    }


    public class GetEntityQueryValidator : AbstractValidator<GetEntityQuery>
    {
        public GetEntityQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}