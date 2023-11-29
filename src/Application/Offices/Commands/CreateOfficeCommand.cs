using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class CreateOfficeCommand : IRequest<OfficeDto>
{
    public required int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, OfficeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateOfficeCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OfficeDto> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var office = new Office() { Id = request.Id, NameRu = request.NameRu, NameKg = request.NameKg };

            _context.Offices.Add(office);
            await _context.SaveChangesAsync(cancellationToken);

            if (office.CreatedBy != office.ModifiedBy)
            {
                throw InvalidCreatedModifiedValuesException.Create(office);
            }
            
            var createdByUser = await _context.Entry(office)
                .Reference(x => x.CreatedByUser)
                .Query()
                .Select(x => string.Join(' ', x.LastName, x.FirstName, x.PatronymicName))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var dto = _mapper.Map<OfficeDto>(office);
            dto.CreatedByUser = createdByUser;
            dto.ModifiedByUser = createdByUser;
            
            return dto;
        }
    }

    public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
    {
        public CreateOfficeCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}