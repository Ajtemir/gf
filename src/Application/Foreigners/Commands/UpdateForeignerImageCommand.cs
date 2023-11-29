using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Foreigners.Commands;

public class UpdateForeignerImageCommand : ICommand
{
    public required int Id { get; set; }
    public string? ImageName { get; set; }
    public string? Image { get; set; }
    
    public class UpdateForeignerImageCommandHandler : IRequestHandler<UpdateForeignerImageCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateForeignerImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateForeignerImageCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);

            var foreigner = await _context.Foreigners.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (foreigner is null)
            {
                throw new NotFoundException(nameof(Foreigner), request.Id);
            }

            foreigner.ImageName = request.ImageName;
            foreigner.Image = image;
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


    public class UpdateForeignerImageCommandValidator : AbstractValidator<UpdateForeignerImageCommand>
    {
        public UpdateForeignerImageCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.Image).Image();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
        }
    }
}