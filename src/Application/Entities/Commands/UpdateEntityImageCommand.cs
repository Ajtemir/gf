using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Entities.Commands;

public class UpdateEntityImageCommand : ICommand
{
    public required int Id { get; set; }
    public string? Image { get; set; }
    public string? ImageName { get; set; }

    public class UpdateEntityImageCommandHandler : IRequestHandler<UpdateEntityImageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEntityImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEntityImageCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            
            var entity = await _context.Entities.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Entity), request.Id);
            }

            entity.ImageName = request.ImageName;
            entity.Image = image;
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    public class UpdateEntityImageCommandValidator : AbstractValidator<UpdateEntityImageCommand>
    {
        public UpdateEntityImageCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.Image).Image();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
        }
    }
}