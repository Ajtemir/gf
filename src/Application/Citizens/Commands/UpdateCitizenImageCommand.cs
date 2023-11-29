using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Citizens.Commands;

public class UpdateCitizenImageCommand : ICommand
{
    public required int Id { get; set; }
    public string? ImageName { get; set; }
    public string? Image { get; set; }
    
    public class UpdateCitizenImageCommandHandler : IRequestHandler<UpdateCitizenImageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCitizenImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCitizenImageCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);

            var citizen = await _context.Citizens.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (citizen is null)
            {
                throw new NotFoundException(nameof(Citizen), request.Id);
            }

            citizen.ImageName = request.ImageName;
            citizen.Image = image;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }


    public class UpdateCitizenImageCommandValidator : AbstractValidator<UpdateCitizenImageCommand>
    {
        public UpdateCitizenImageCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.Image).Image();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
        }
    }
}