using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class DeleteOfficeCommand : ICommand
{
    public required int Id { get; set; }
    
    public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteOfficeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = await _context.Offices
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (office is null)
            {
                throw new NotFoundException(nameof(Office), request.Id);
            }

            _context.Offices.Remove(office);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
    public class DeleteOfficeCommandValidator : AbstractValidator<DeleteOfficeCommand>
    {
        public DeleteOfficeCommandValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }

}