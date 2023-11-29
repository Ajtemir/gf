using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Dictionary;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Commands;

public class UpdatePositionCommand : ICommand
{
    public required int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePositionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = await _context.Positions.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (position is null)
            {
                throw new NotFoundException(nameof(Position), request.Id);
            }

            position.NameRu = request.NameRu;
            position.NameKg = request.NameKg;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}
