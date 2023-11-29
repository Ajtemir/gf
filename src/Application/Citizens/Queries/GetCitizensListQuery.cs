using Application.Common.Dto;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Citizens.Queries;

public class GetCitizensListQuery : IRequest<IEnumerable<CitizenDto>>
{
    public class GetCitizensListQueryHandler : IRequestHandler<GetCitizensListQuery, IEnumerable<CitizenDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCitizensListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CitizenDto>> Handle(GetCitizensListQuery request, CancellationToken cancellationToken)
        {
            var citizens = await _context.Citizens
                .ProjectToListAsync<CitizenDto>(_mapper.ConfigurationProvider, cancellationToken);
            
            return citizens;
        }
    }
}