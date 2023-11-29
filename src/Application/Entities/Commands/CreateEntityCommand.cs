using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Entities.Commands;

public class CreateEntityCommand : IRequest<EntityDto>
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    public string? Image { get; set; }
    public string? ImageName { get; set; }
    
    public class CreateEntityCommandHandler : IRequestHandler<CreateEntityCommand, EntityDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEntityCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EntityDto> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            var entity = new Entity
            {
                NameRu = request.NameRu,
                NameKg = request.NameKg,
                ImageName = request.ImageName,
                Image = image,
            };
            
            _context.Entities.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            if (entity.CreatedBy != entity.ModifiedBy)
            {
                throw InvalidCreatedModifiedValuesException.Create(entity);
            }
            
            var createdByUser = await _context.Entry(entity)
                .Reference(x => x.CreatedByUser)
                .Query()
                .Select(x => string.Join(' ', x.LastName, x.FirstName, x.PatronymicName))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var dto = _mapper.Map<EntityDto>(entity);
            dto.CreatedByUser = createdByUser;
            dto.ModifiedByUser = createdByUser;

            return dto;
        }
    }

    public class CreateEntityCommandValidator : AbstractValidator<CreateEntityCommand>
    {
        public CreateEntityCommandValidator()
        {
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
            RuleFor(x => x.Image).Image();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
        }
    }
}