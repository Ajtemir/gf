using Application.Common.Dto;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Queries;

public class GetPositionListQuery : IRequest<IList<PositionDto>>
{
    public class GetPositionListQueryHandler : IRequestHandler<GetPositionListQuery, IList<PositionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPositionListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<PositionDto>> Handle(GetPositionListQuery request, CancellationToken cancellationToken)
        {
            var positions = await _context.Positions.AsNoTracking()
                .ProjectTo<PositionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return positions;
        }
    }
}