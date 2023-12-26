using Application.Common.Dto.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class UpdateOfficeCommand : ICommand
{
    public required int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    public required List<Identifier> ParentOffices { get; set; } = new();

    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOfficeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _context.Offices.Include(x=>x.ParentOffices).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (office is null)
            {
                throw new NotFoundException(nameof(Office), request.Id);
            }

            office.NameKg = request.NameKg;
            office.NameRu = request.NameRu;
            office.ParentOffices = new List<OfficeRelationship>();
            request.ParentOffices.ForEach(x =>
            {
                office.ParentOffices.Add(new OfficeRelationship
                {
                    ChildOfficeId = office.Id,
                    ParentOfficeId = x.Id,
                });
            });
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public class UpdateOfficeCommandValidator : AbstractValidator<UpdateOfficeCommand>
    {
        public UpdateOfficeCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            // RuleFor(x => x.NewId).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}
