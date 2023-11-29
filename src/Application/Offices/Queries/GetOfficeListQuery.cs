using Application.Common.Dto;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Offices.Queries;

public class GetOfficeListQuery : IRequest<IEnumerable<OfficeDto>>
{
    public class GetOfficeListQueryHandler : IRequestHandler<GetOfficeListQuery, IEnumerable<OfficeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOfficeListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OfficeDto>> Handle(GetOfficeListQuery request, CancellationToken cancellationToken)
        {
            var offices = await _context.Offices.ProjectToListAsync<OfficeDto>(_mapper.ConfigurationProvider, cancellationToken: cancellationToken);
            return offices;
        }
    }

}