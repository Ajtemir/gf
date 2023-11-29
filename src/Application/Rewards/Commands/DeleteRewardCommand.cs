using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Rewards.Commands;

public class DeleteRewardCommand : ICommand
{
    public int Id { get; set; }
    
    public class DeleteRewardCommandHandler : IRequestHandler<DeleteRewardCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRewardCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRewardCommand request, CancellationToken cancellationToken)
        {
            var affectedRows = await _context.Rewards
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            if (affectedRows == 0)
            {
                throw new NotFoundException(nameof(Reward), request.Id);
            }

            return Unit.Value;
        }
    }

    public class DeleteRewardCommandValidator : AbstractValidator<DeleteRewardCommand>
    {
        public DeleteRewardCommandValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}