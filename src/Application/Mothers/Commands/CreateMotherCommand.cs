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

namespace Application.Mothers.Commands;

public class CreateMotherCommand : IRequest<MotherDto>
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required string Pin { get; set; }
    public required string PassportNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? Image { get; set; }
    public string? ImageName { get; set; }
    public required string RegisteredAddress { get; set; }
    public string? ActualAddress { get; set; }
    
    public class CreateMotherCommandHandler : IRequestHandler<CreateMotherCommand, MotherDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateMotherCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MotherDto> Handle(CreateMotherCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            var member = await _context.Members.FirstOrDefaultAsync(x => x.Pin == request.Pin, cancellationToken: cancellationToken);
            if (member == null)
            {
                member = new Member { Pin = request.Pin };
                _context.Members.Add(member);
                await _context.SaveChangesAsync(cancellationToken);
            }
            var mother = new Mother
            {
                CandidateTypeId = CandidateTypes.Mother,
                // LastName = request.LastName,
                // FirstName = request.FirstName,
                // PatronymicName = request.PatronymicName,
                // PassportNumber = request.PassportNumber,
                // Gender = Gender.Female,
                // BirthDate = request.BirthDate,
                // DeathDate = request.DeathDate,
                // RegisteredAddress = request.RegisteredAddress,
                // ActualAddress = request.ActualAddress,
                ImageName = request.ImageName,
                Image = image,
                PersonId = member.Id,
            };
            
            _context.Mothers.Add(mother);
            await _context.SaveChangesAsync(cancellationToken);

            if (mother.CreatedBy != mother.ModifiedBy)
            {
                throw InvalidCreatedModifiedValuesException.Create(mother);
            }
            
            var createdByUser = await _context.Entry(mother)
                .Reference(x => x.CreatedByUser)
                .Query()
                .Select(x => string.Join(' ', x.LastName, x.FirstName, x.PatronymicName))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var dto = _mapper.Map<MotherDto>(mother);
            dto.CreatedByUser = createdByUser;
            dto.ModifiedByUser = createdByUser;

            return dto;
        }
    }

    public class CreateMotherCommandValidator : AbstractValidator<CreateMotherCommand>
    {
        public CreateMotherCommandValidator()
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
        }
    }
}