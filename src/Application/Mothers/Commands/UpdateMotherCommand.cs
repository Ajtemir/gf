using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Mothers.Commands;

public class UpdateMotherCommand : ICommand
{
    public int Id { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public required string Pin { get; set; }
    public required string PassportNumber { get; set; }
    public string? PatronymicName { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public required string RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }

    public class UpdateMotherCommandHandler : IRequestHandler<UpdateMotherCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMotherCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMotherCommand request, CancellationToken cancellationToken)
        {
            var mother = await _context.Mothers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (mother is null)
            {
                throw new NotFoundException(nameof(Mother), request.Id);
            }
            
            // mother.LastName = request.LastName;
            // mother.FirstName = request.FirstName;
            // mother.PatronymicName = request.PatronymicName;
            // mother.PassportNumber = request.PassportNumber;
            // mother.BirthDate = request.BirthDate;
            // mother.DeathDate = request.DeathDate;
            // mother.RegisteredAddress = request.RegisteredAddress;
            // mother.ActualAddress = request.ActualAddress;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }


    public class UpdateMotherCommandValidator : AbstractValidator<UpdateMotherCommand>
    {
        public UpdateMotherCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.PatronymicName).PatronymicName();
            RuleFor(x => x.Pin).Pin();
            RuleFor(x => x.PassportNumber).PassportNumber();
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.RegisteredAddress).NotEmpty().MaximumLength(256);
            RuleFor(x => x.ActualAddress).MaximumLength(256);
        }
    }
}