using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Application.Offices.Commands;

public class AddChildOfficeCommand : ICommand
{
    public required int OfficeId { get; set; }
    public required int ChildOfficeId { get; set; }

    public class AddChildOfficeCommandHandler : IRequestHandler<AddChildOfficeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public AddChildOfficeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddChildOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _context.Offices.Where(x => x.Id == request.OfficeId)
                .Include(x => x.ChildOffices)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (office is null)
            {
                throw new NotFoundException(nameof(Office), request.OfficeId);
            }

            var childRelationship = new OfficeRelationship()
            {
                ChildOfficeId = request.ChildOfficeId, ParentOfficeId = request.OfficeId,
            };

            if (office.ChildOffices.Contains(childRelationship))
            {
                return Unit.Value;
            }

            office.ChildOffices.Add(childRelationship);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class AddChildOfficeCommandValidator : AbstractValidator<AddChildOfficeCommand>
    {
        public AddChildOfficeCommandValidator()
        {
            RuleFor(x => x.OfficeId).Id();
            RuleFor(x => x.ChildOfficeId).Id();
        }
    }
}