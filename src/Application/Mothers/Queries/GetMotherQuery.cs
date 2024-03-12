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

namespace Application.Mothers.Queries;

public class GetMotherQuery : IRequest<MotherDto>
{
    public int Id { get; set; }

    public class GetMotherQueryHandler : IRequestHandler<GetMotherQuery, MotherDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMotherQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MotherDto> Handle(GetMotherQuery request, CancellationToken cancellationToken)
        {
            var motherDto = await _context.Mothers.AsNoTracking()
                .Include(x=>x.Person)
                .Where(x => x.Id == request.Id)
                .ProjectTo<MotherDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (motherDto is null)
            {
                throw new NotFoundException(nameof(Mother), request.Id);
            }
            
            return motherDto;
        }
    }

    public class GetMotherQueryValidator : AbstractValidator<GetMotherQuery>
    {
        public GetMotherQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}