using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Rewards.Commands;

public class UpdateRewardDetailsCommand : ICommand
{
    public required int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class UpdateRewardDetailsCommandHandler : IRequestHandler<UpdateRewardDetailsCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateRewardDetailsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRewardDetailsCommand request, CancellationToken cancellationToken)
        {
            var reward = await _context.Rewards.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (reward is null)
            {
                throw new NotFoundException(nameof(Reward), request.Id);
            }

            reward.NameRu = request.NameRu;
            reward.NameKg = request.NameKg;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    public class UpdateRewardDetailsCommandValidator : AbstractValidator<UpdateRewardDetailsCommand>
    {
        public UpdateRewardDetailsCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}