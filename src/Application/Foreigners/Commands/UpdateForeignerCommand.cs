using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Foreigners.Commands;

public class UpdateForeignerCommand : ICommand
{
    public required int Id { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public required Gender Gender { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly? DeathDate { get; set; }
    public string? Image { get; set; }
    public string? ImageName { get; set; }
    public int CitizenshipId { get; set; }
    
    public class UpdateForeignerCommandHandler : IRequestHandler<UpdateForeignerCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateForeignerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateForeignerCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);

            var foreigner = await _context.Foreigners.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            
            if (foreigner is null)
            {
                throw new NotFoundException(nameof(Foreigner), request.Id);
            }
            
            foreigner.LastName = request.LastName;
            foreigner.FirstName = request.FirstName;
            foreigner.PatronymicName = request.PatronymicName;
            foreigner.Gender = request.Gender;
            foreigner.BirthDate = request.BirthDate;
            foreigner.DeathDate = request.DeathDate;
            foreigner.CitizenshipId = request.CitizenshipId;
            foreigner.ImageName = request.ImageName;
            foreigner.Image = image;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateForeignerCommandValidator : AbstractValidator<UpdateForeignerCommand>
    {
        public UpdateForeignerCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.PatronymicName).PatronymicName();
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.Image).Image();
            RuleFor(x => x.ImageName).RequiredNameWhenFieldNotNull(x => x.Image);
            RuleFor(x => x.CitizenshipId).Id();
        }
    }

}