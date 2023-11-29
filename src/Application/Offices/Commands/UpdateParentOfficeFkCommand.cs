using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class UpdateParentOfficeFkCommand : ICommand
{
    public required int OfficeId { get; set; }
    public IEnumerable<int> ParentOfficeIds { get; set; } = Enumerable.Empty<int>();
    
    public class UpdateParentOfficeFkCommandHandler : IRequestHandler<UpdateParentOfficeFkCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateParentOfficeFkCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateParentOfficeFkCommand request, CancellationToken cancellationToken)
        {
            var office = await FindOffice(request.OfficeId, cancellationToken);
            await ClearParentFk(office, cancellationToken);
            await AddParentFk(office, request.ParentOfficeIds, cancellationToken);

            return Unit.Value;
        }

        private async Task<Office> FindOffice(int officeId, CancellationToken cancellationToken)
        {
            var office = await _context.Offices
                .Where(x => x.Id == officeId)
                .Include(x => x.ParentOffices)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (office is null)
            {
                throw new NotFoundException(nameof(Office), officeId);
            }

            return office;
        }

        private async Task ClearParentFk(Office office, CancellationToken cancellationToken)
        {
            if (office.ParentOffices.Any())
            {
                office.ParentOffices.Clear();
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task AddParentFk(Office office, IEnumerable<int> parentOfficeIds, CancellationToken cancellationToken)
        {
            var newRelationships = parentOfficeIds.Select(parentOfficeId => new OfficeRelationship()
            {
                ChildOfficeId = office.Id, ParentOfficeId = parentOfficeId
            });

            if (office.ParentOffices is List<OfficeRelationship> relationships)
            {
                relationships.AddRange(newRelationships);
            }
            else
            {
                foreach (var officeRelationship in newRelationships)
                {
                    office.ParentOffices.Add(officeRelationship);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public class UpdateParentOfficeFkCommandValidator : AbstractValidator<UpdateParentOfficeFkCommand>
    {
        public UpdateParentOfficeFkCommandValidator()
        {
            RuleFor(x => x.OfficeId).Id();
        }
    }
}