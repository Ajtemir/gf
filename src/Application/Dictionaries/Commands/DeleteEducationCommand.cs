using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Dictionary;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Commands;

public class DeleteEducationCommand : ICommand
{
    public int Id { get; set; }
    
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEducationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var affectedRows = await _context.Educations
                .Where(x => x.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            if (affectedRows == 0)
            {
                throw new NotFoundException(nameof(Education), request.Id);
            }
            
            return Unit.Value;
        }
    }

    public class DeleteEducationCommandValidator : AbstractValidator<DeleteEducationCommand>
    {
        public DeleteEducationCommandValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}