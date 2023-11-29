using Application.Common.Dto;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Mothers.Queries;

public class GetMotherListQuery : IRequest<IEnumerable<MotherDto>>
{
    public class GetMotherListQueryHandler : IRequestHandler<GetMotherListQuery, IEnumerable<MotherDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMotherListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MotherDto>> Handle(GetMotherListQuery request, CancellationToken cancellationToken)
        {
            var mothers = await _context.Mothers
                .ProjectToListAsync<MotherDto>(_mapper.ConfigurationProvider, cancellationToken);
            
            return mothers;
        }
    }
}