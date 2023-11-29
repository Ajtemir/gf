using Application.Common.Dto;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Foreigners.Queries;

public class GetForeignerListQuery : IRequest<IEnumerable<ForeignerDto>>
{
    public class GetForeignerListQueryHandler : IRequestHandler<GetForeignerListQuery, IEnumerable<ForeignerDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetForeignerListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ForeignerDto>> Handle(GetForeignerListQuery request, CancellationToken cancellationToken)
        {
            var foreigners = await _context.Foreigners
                .ProjectToListAsync<ForeignerDto>(_mapper.ConfigurationProvider, cancellationToken);

            return foreigners;
        }
    }
}