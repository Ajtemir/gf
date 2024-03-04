using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Foreigners.Commands;

public class CreateForeignerCommand : IRequest<ForeignerDto>
{
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required Gender Gender { get; set; }
    public required DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public required int CitizenshipId { get; set; }
    
    public class CreateForeignerCommandHandler : IRequestHandler<CreateForeignerCommand, ForeignerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateForeignerCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ForeignerDto> Handle(CreateForeignerCommand request, CancellationToken cancellationToken)
        {
            var member = new Member();
            _context.Members.Add(member);
            await _context.SaveChangesAsync(cancellationToken);
            var foreigner = new Foreigner
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                PatronymicName = request.PatronymicName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                DeathDate = request.DeathDate,
                CitizenshipId = request.CitizenshipId,
                MemberId = member.Id,
                PassportNumber = null,
                RegisteredAddress = null,
            };
            

            _context.Foreigners.Add(foreigner);
            await _context.SaveChangesAsync(cancellationToken);
            
            if (foreigner.CreatedBy != foreigner.ModifiedBy)
            {
                throw InvalidCreatedModifiedValuesException.Create(foreigner);
            }
            
            var createdByUser = await _context.Entry(foreigner)
                .Reference(x => x.CreatedByUser)
                .Query()
                .Select(x => string.Join(' ', x.LastName, x.FirstName, x.PatronymicName))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var citizenship = await _context.Entry(foreigner)
                .Reference(x => x.Citizenship)
                .Query()
                .Select(c => new { c.NameRu, c.NameKg })
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var dto = _mapper.Map<ForeignerDto>(foreigner);
            dto.CreatedByUser = createdByUser;
            dto.ModifiedByUser = createdByUser;
            dto.CitizenshipRu = citizenship?.NameRu;
            dto.CitizenshipKg = citizenship?.NameKg;
            
            return dto;
        }
    }


    public class CreateForeignerCommandValidator : AbstractValidator<CreateForeignerCommand>
    {
        public CreateForeignerCommandValidator()
        {
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.PatronymicName).PatronymicName();
            RuleFor(x => x.BirthDate).NotEmpty();
            // RuleFor(x => x.Deathday).NotEmpty();
            RuleFor(x => x.CitizenshipId).Id();
        }
    }
}
