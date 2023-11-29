using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Dictionary;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Commands;

public class DeleteCitizenshipCommand : ICommand
{
    public int Id { get; set; }
    
    public class DeleteCitizenshipCommandHandler : IRequestHandler<DeleteCitizenshipCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCitizenshipCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCitizenshipCommand request, CancellationToken cancellationToken)
        {
            var affectedRows = await _context.Citizenships
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            if (affectedRows == 0)
            {
                throw new NotFoundException(nameof(Citizenship), request.Id);
            }
            
            return Unit.Value;
        }
    }

    public class DeleteCitizenshipCommandValidator : AbstractValidator<DeleteCitizenshipCommand>
    {
        public DeleteCitizenshipCommandValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}