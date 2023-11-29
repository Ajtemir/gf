using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Mothers.Commands;

public class UpdateMotherImageCommand : ICommand
{
    public required int Id { get; set; }
    public string? ImageName { get; set; }
    public string? Image { get; set; }

    public class UpdateMotherImageCommandHandler : IRequestHandler<UpdateMotherImageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMotherImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMotherImageCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            
            var mother = await _context.Mothers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (mother is null)
            {
                throw new NotFoundException(nameof(Mother), request.Id);
            }

            mother.ImageName = request.ImageName;
            mother.Image = image;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateMotherImageCommandValidator : AbstractValidator<UpdateMotherImageCommand>
    {
        public UpdateMotherImageCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.Image).Image();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
        }
    }
}