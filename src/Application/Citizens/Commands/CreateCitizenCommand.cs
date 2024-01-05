using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Citizens.Commands;

public class CreateCitizenCommand : IRequest<CitizenDto>
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required string Pin { get; set; }
    public required string PassportNumber { get; set; }
    public required Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? Image { get; set; }
    public string? ImageName { get; set; }
    
    public required string RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
    public required int EducationId { get; set; }
    public string? ScienceDegree { get; set; }
    public required int YearsOfWorkTotal { get; set; }
    public required int YearsOfWorkInIndustry { get; set; }
    public required int YearsOfWorkInCollective { get; set; }
    
    public class CreateCitizenCommandHandler : IRequestHandler<CreateCitizenCommand, CitizenDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCitizenCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitizenDto> Handle(CreateCitizenCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Pin == request.Pin, cancellationToken: cancellationToken);
            if (member == null)
            {
                member = new Member { Pin = request.Pin };
                _context.Members.Add(member);
                await _context.SaveChangesAsync(cancellationToken);
            }
            var citizen = new Citizen
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                PatronymicName = request.PatronymicName,
                MemberId = member.Id,
                PassportNumber = request.PassportNumber,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                DeathDate = request.DeathDate,
                RegisteredAddress = request.RegisteredAddress,
                ActualAddress = request.ActualAddress,
                EducationId = request.EducationId,
                ScienceDegree = request.ScienceDegree,
                YearsOfWorkTotal = request.YearsOfWorkTotal,
                YearsOfWorkInIndustry = request.YearsOfWorkInIndustry,
                YearsOfWorkInCollective = request.YearsOfWorkInCollective,
                ImageName = request.ImageName,
                Image = image,
            };

            _context.Citizens.Add(citizen);
            await _context.SaveChangesAsync(cancellationToken);

            if (citizen.CreatedBy != citizen.ModifiedBy)
            {
                throw InvalidCreatedModifiedValuesException.Create(citizen);
            }
            
            var createdByUser = await _context.Entry(citizen)
                .Reference(x => x.CreatedByUser)
                .Query()
                .Select(x => string.Join(' ', x.LastName, x.FirstName, x.PatronymicName))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var dto = _mapper.Map<CitizenDto>(citizen);
            dto.CreatedByUser = createdByUser;
            dto.ModifiedByUser = createdByUser;
            
            return dto;
        }
    }

    public class CreateCitizenCommandValidator : AbstractValidator<CreateCitizenCommand>
    {
        public CreateCitizenCommandValidator()
        {
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.PatronymicName).PatronymicName();
            RuleFor(x => x.Pin).Pin();
            RuleFor(x => x.PassportNumber).PassportNumber();
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.Image).Image();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);

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

            RuleFor(x => x.YearsOfWorkInCollective).GreaterThanOrEqualTo(0);
        }
    }
}