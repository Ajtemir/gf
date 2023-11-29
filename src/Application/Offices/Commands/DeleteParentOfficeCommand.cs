using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class DeleteParentOfficeCommand : ICommand
{
    public required int OfficeId { get; set; }
    public required int ParentOfficeId { get; set; }

    public class DeleteParentOfficeCommandHandler : IRequestHandler<DeleteParentOfficeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteParentOfficeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteParentOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _context.Offices.Where(x => x.Id == request.OfficeId)
                .Include(x => x.ParentOffices)
                .FirstOrDefaultAsync(cancellationToken);

            if (office is null)
            {
                throw new NotFoundException(nameof(Office), request.OfficeId);
            }

            var parentRelationship = new OfficeRelationship()
            {
                ChildOfficeId = request.OfficeId, ParentOfficeId = request.ParentOfficeId,
            };
            
            var containsParentOffice = office.ParentOffices.Contains(parentRelationship);
            if (!containsParentOffice)
            {
                return Unit.Value;
            }

            office.ParentOffices.Remove(parentRelationship);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    public class DeleteParentOfficeCommandValidator : AbstractValidator<DeleteParentOfficeCommand>
    {
        public DeleteParentOfficeCommandValidator()
        {
            RuleFor(x => x.OfficeId).Id();
            RuleFor(x => x.ParentOfficeId).Id();
        }
    }
}