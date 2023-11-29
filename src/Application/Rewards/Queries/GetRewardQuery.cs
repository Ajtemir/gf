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

namespace Application.Rewards.Queries;

public class GetRewardQuery : IRequest<RewardDto>
{
    public int Id { get; set; }
    
    public class GetRewardQueryHandler : IRequestHandler<GetRewardQuery, RewardDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRewardQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RewardDto> Handle(GetRewardQuery request, CancellationToken cancellationToken)
        {
            var rewardDto = await _context.Rewards.AsNoTracking()
                .ProjectTo<RewardDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (rewardDto is null)
            {
                throw new NotFoundException(nameof(Reward), request.Id);
            }

            return rewardDto;
        }
    }


    public class GetRewardQueryValidator : AbstractValidator<GetRewardQuery>
    {
        public GetRewardQueryValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}