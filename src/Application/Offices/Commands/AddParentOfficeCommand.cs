using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class AddParentOfficeCommand : ICommand
{
    public required int OfficeId { get; set; }
    public required int ParentOfficeId { get; set; }
    
    public class AddParentOfficeCommandHandler : IRequestHandler<AddParentOfficeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public AddParentOfficeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddParentOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _context.Offices.Where(x => x.Id == request.OfficeId)
                .Include(x => x.ParentOffices)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (office is null)
            {
                throw new NotFoundException(nameof(Office), request.OfficeId);
            }

            var parentRelationship = new OfficeRelationship()
            {
                ChildOfficeId = request.OfficeId, ParentOfficeId = request.ParentOfficeId,
            };

            if (office.ParentOffices.Contains(parentRelationship))
            {
                return Unit.Value;
            }
            
            office.ParentOffices.Add(parentRelationship);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    public class AddParentOfficeCommandValidator : AbstractValidator<AddParentOfficeCommand>
    {
        public AddParentOfficeCommandValidator()
        {
            RuleFor(x => x.OfficeId).Id();
            RuleFor(x => x.ParentOfficeId).Id();
        }
    }
}