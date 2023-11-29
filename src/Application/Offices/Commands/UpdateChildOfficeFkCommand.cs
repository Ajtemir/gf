using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class UpdateChildOfficeFkCommand : ICommand
{
    public required int OfficeId { get; set; }
    public IEnumerable<int> ChildOfficeIds { get; set; } = Enumerable.Empty<int>();

    public class UpdateChildOfficeKfCommandHandler : IRequestHandler<UpdateChildOfficeFkCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateChildOfficeKfCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateChildOfficeFkCommand request, CancellationToken cancellationToken)
        {
            var office = await FindOffice(request.OfficeId, cancellationToken);
            await ClearChildFk(office, cancellationToken);
            await AddNewChildFk(office, request.ChildOfficeIds, cancellationToken);

            return Unit.Value;
        }

        private async Task<Office> FindOffice(int officeId, CancellationToken cancellationToken)
        {
            var office = await _context.Offices
                .Where(x => x.Id == officeId)
                .Include(x => x.ChildOffices)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (office is null)
            {
                throw new NotFoundException(nameof(Office), officeId);
            }

            return office;
        }

        private async Task ClearChildFk(Office office, CancellationToken cancellationToken)
        {
            if (office.ChildOffices.Any())
            {
                office.ChildOffices.Clear();
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task AddNewChildFk(Office office, IEnumerable<int> childOfficeIds, CancellationToken cancellationToken)
        {
            var newRelationships = childOfficeIds.Select(childOfficeId => new OfficeRelationship()
            {
                ChildOfficeId = childOfficeId,
                ParentOfficeId = office.Id,
            });
            
            if (office.ChildOffices is List<OfficeRelationship> relationships)
            {
                relationships.AddRange(newRelationships);
            }
            else
            {
                foreach (var officeRelationship in newRelationships)
                {
                    office.ChildOffices.Add(officeRelationship);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class UpdateChildOfficeFkCommandValidator : AbstractValidator<UpdateChildOfficeFkCommand>
    {
        public UpdateChildOfficeFkCommandValidator()
        {
            RuleFor(x => x.OfficeId).Id();
        }
    }
}
