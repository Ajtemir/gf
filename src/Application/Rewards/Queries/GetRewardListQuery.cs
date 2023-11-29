using Application.Common.Dto;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Rewards.Queries;

public class GetRewardListQuery : IRequest<IEnumerable<RewardDto>>
{
    public class GetRewardListQueryHandler : IRequestHandler<GetRewardListQuery, IEnumerable<RewardDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRewardListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RewardDto>> Handle(GetRewardListQuery request, CancellationToken cancellationToken)
        {
            var rewards = await _context.Rewards
                .ProjectToListAsync<RewardDto>(_mapper.ConfigurationProvider, cancellationToken);

            return rewards;
        }
    }
}