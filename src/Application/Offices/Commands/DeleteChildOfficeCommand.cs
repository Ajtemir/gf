using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class DeleteChildOfficeCommand : ICommand
{
    public required int OfficeId { get; set; }
    public required int ChildOfficeId { get; set; }

    public class DeleteChildOfficeCommandHandler : IRequestHandler<DeleteChildOfficeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteChildOfficeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteChildOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _context.Offices.Where(x => x.Id == request.OfficeId)
                .Include(x => x.ChildOffices)
                .FirstOrDefaultAsync(cancellationToken);

            if (office is null)
            {
                throw new NotFoundException(nameof(Office), request.OfficeId);
            }

            var childRelationship = new OfficeRelationship()
            {
                ChildOfficeId = request.ChildOfficeId,
                ParentOfficeId = request.OfficeId,
            };

            var containsChildRelationship = office.ChildOffices.Contains(childRelationship);
            if (!containsChildRelationship)
            {
                return Unit.Value;
            }

            office.ChildOffices.Remove(childRelationship);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class DeleteChildOfficeCommandValidator : AbstractValidator<DeleteChildOfficeCommand>
    {
        public DeleteChildOfficeCommandValidator()
        {
            RuleFor(x => x.OfficeId).Id();
            RuleFor(x => x.ChildOfficeId).Id();
        }
    }
}