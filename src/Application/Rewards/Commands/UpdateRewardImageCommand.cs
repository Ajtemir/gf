using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Rewards.Commands;

public class UpdateRewardImageCommand : ICommand
{
    public required int Id { get; set; }
    public required string Image { get; set; }
    public required string ImageName { get; set; }
    
    public class UpdateRewardImageCommandHandler : IRequestHandler<UpdateRewardImageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRewardImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRewardImageCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            var reward = await _context.Rewards.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (reward is null)
            {
                throw new NotFoundException(nameof(Reward), request.Id);
            }

            reward.Image = image;
            reward.ImageName = request.ImageName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateRewardImageCommandValidator : AbstractValidator<UpdateRewardImageCommand>
    {
        public UpdateRewardImageCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
        }
    }
}