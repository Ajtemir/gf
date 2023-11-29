using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Citizens.Commands;

public class UpdateCitizenCommand : ICommand
{
    public required int Id { get; set; }

    public required string LastName { get; set; }

    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required string Pin { get; set; }
    public required string PassportNumber { get; set; }
    public required Gender Gender { get; set; }

    public DateOnly BirthDate { get; set; }

    public DateOnly? DeathDate { get; set; }

    public required string RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
    public int EducationId { get; set; }
    public string? ScienceDegree { get; set; }
    public int YearsOfWorkTotal { get; set; }
    public int YearsOfWorkInIndustry { get; set; }
    public int YearsOfWorkInCollective { get; set; }

    public class UpdateCitizenCommandHandler : IRequestHandler<UpdateCitizenCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCitizenCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCitizenCommand request, CancellationToken cancellationToken)
        {
            var citizen = await _context.Citizens.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (citizen is null)
            {
                throw new NotFoundException(nameof(Citizen), request.Id);
            }

            citizen.LastName = request.LastName;
            citizen.FirstName = request.FirstName;
            citizen.PatronymicName = request.PatronymicName;
            citizen.Pin = request.Pin;
            citizen.PassportNumber = request.PassportNumber;
            citizen.Gender = request.Gender;
            citizen.BirthDate = request.BirthDate;
            citizen.DeathDate = request.DeathDate;
            citizen.RegisteredAddress = request.RegisteredAddress;
            citizen.ActualAddress = request.ActualAddress;
            citizen.EducationId = request.EducationId;
            citizen.ScienceDegree = request.ScienceDegree;
            citizen.YearsOfWorkTotal = request.YearsOfWorkTotal;
            citizen.YearsOfWorkInIndustry = request.YearsOfWorkInIndustry;
            citizen.YearsOfWorkInCollective = request.YearsOfWorkInCollective;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateCitizenCommandValidator : AbstractValidator<UpdateCitizenCommand>
    {
        public UpdateCitizenCommandValidator()
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
            RuleFor(x => x.EducationId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ScienceDegree).MaximumLength(256);
            RuleFor(x => x.RegisteredAddress).NotEmpty().MaximumLength(256);
            RuleFor(x => x.ActualAddress).MaximumLength(256);

            RuleFor(x => x.YearsOfWorkTotal).NotEmpty()
                .Must((citizen, totalYears) => totalYears >= citizen.YearsOfWorkInIndustry)
                .WithMessage("Общий стаж работы должен быть больше, чем стаж работы в индустрии.");

            RuleFor(x => x.YearsOfWorkInIndustry).NotEmpty()
                .Must((citizen, yearsInIndustry) => yearsInIndustry >= citizen.YearsOfWorkInCollective)
                .WithMessage("Стаж работы в индустрии должен быть больше, чем стаж работы в коллективе.");

            RuleFor(x => x.YearsOfWorkInCollective).GreaterThan(0);
        }
    }
}