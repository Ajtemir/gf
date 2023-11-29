using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Candidates.Commands;

public class DeleteCandidateCommand : ICommand
{
    public required int Id { get; set; }
    
    public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCandidateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _context.Candidates.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (candidate is null)
            {
                throw new NotFoundException(nameof(Candidate), request.Id);
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class DeleteCandidateCommandValidator : AbstractValidator<DeleteCandidateCommand>
    {
        public DeleteCandidateCommandValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}