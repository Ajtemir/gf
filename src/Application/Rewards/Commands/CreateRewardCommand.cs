using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rewards.Commands;

public class CreateRewardCommand : IRequest<RewardDto>
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public required string Image { get; set; }
    public required string ImageName { get; set; }
    
    public class CreateRewardCommandHandler : IRequestHandler<CreateRewardCommand, RewardDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateRewardCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RewardDto> Handle(CreateRewardCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            var reward = new Reward
            {
                NameRu = request.NameRu,
                NameKg = request.NameKg,
                ImageName = request.ImageName,
                Image = image,
            };

            _context.Rewards.Add(reward);
            await _context.SaveChangesAsync(cancellationToken);

            if (reward.CreatedBy != reward.ModifiedBy)
            {
                throw InvalidCreatedModifiedValuesException.Create(reward);
            }
            
            var createdByUser = await _context.Entry(reward)
                .Reference(x => x.CreatedByUser)
                .Query()
                .Select(x => string.Join(' ', x.LastName, x.FirstName, x.PatronymicName))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var dto = _mapper.Map<RewardDto>(reward);
            dto.CreatedByUser = createdByUser;
            dto.ModifiedByUser = createdByUser;

            return dto;
        }
    }

    public class CreateRewardCommandValidator : AbstractValidator<CreateRewardCommand>
    {
        public CreateRewardCommandValidator()
        {
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
            RuleFor(x => x.Image).Image().NotEmpty();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
        }
    }
}