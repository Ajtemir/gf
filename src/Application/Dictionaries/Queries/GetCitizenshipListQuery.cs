using Application.Common.Dto;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Queries;
public class GetCitizenshipListQuery : IRequest<IList<CitizenshipDto>>
{
    public class GetCitizenshipListQueryHandler : IRequestHandler<GetCitizenshipListQuery, IList<CitizenshipDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitizenshipListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<CitizenshipDto>> Handle(GetCitizenshipListQuery request, CancellationToken cancellationToken)
        {
            var positions = await _context.Citizenships.AsNoTracking()
                .ProjectTo<CitizenshipDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return positions;
        }
    }
}
