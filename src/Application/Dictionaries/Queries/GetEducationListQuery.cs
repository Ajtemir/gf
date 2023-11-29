using Application.Common.Dto;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Queries;

public class GetEducationListQuery : IRequest<IList<EducationDto>>
{
    public class GetEducationListQueryHandler : IRequestHandler<GetEducationListQuery, IList<EducationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEducationListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<EducationDto>> Handle(GetEducationListQuery request, CancellationToken cancellationToken)
        {
            var positions = await _context.Educations.AsNoTracking()
                .ProjectTo<EducationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return positions;
        }
    }
}
