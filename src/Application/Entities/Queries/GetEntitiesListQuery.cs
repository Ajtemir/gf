using Application.Common.Dto;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Entities.Queries;

public class GetEntitiesListQuery : IRequest<IEnumerable<EntityDto>>
{
    public class GetEntitiesListQueryHandler : IRequestHandler<GetEntitiesListQuery, IEnumerable<EntityDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEntitiesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EntityDto>> Handle(GetEntitiesListQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Entities
                .ProjectToListAsync<EntityDto>(_mapper.ConfigurationProvider, cancellationToken);

            return entities;
        }
    }
}