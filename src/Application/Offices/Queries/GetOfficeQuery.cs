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

namespace Application.Offices.Queries;

public class GetOfficeQuery : IRequest<OfficeDto>
{
    public required int Id { get; set; }
    public bool IncludeChildOffices { get; set; } = true;
    public bool IncludeParentOffices { get; set; } = true;

    public class GetOfficeQueryHandler : IRequestHandler<GetOfficeQuery, OfficeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOfficeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OfficeDto> Handle(GetOfficeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Offices.AsQueryable();
            if (request.IncludeChildOffices)
            {
                query = query.Include(x => x.ChildOffices).ThenInclude(x=> x.ChildOffice);
            }
            
            if (request.IncludeParentOffices)
            {
                query = query.Include(x => x.ParentOffices).ThenInclude(x => x.ParentOffice);
            }

            var officeDto = await query
                .Where(x => x.Id == request.Id)
                .ProjectTo<OfficeDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            if (officeDto is null)
            {
                throw new NotFoundException(nameof(Office), request.Id);
            }

            return officeDto;
        }
    }

    public class GetOfficeQueryValidator : AbstractValidator<GetOfficeQuery>
    {
        public GetOfficeQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}