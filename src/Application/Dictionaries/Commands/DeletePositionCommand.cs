using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Dictionary;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Commands;

public class DeletePositionCommand : ICommand
{
    public int Id { get; set; }
    
    public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeletePositionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var affectedRows = await _context.Positions
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);
            
            if (affectedRows == 0)
            {
                throw new NotFoundException(nameof(Position), request.Id);
            }
            
            return Unit.Value;
        }
    }

    public class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
    {
        public DeletePositionCommandValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}