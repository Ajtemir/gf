using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Dictionary;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Commands;

public class UpdateCitizenshipCommand : ICommand
{
    public required int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class UpdateCitizenshipCommandHandler : IRequestHandler<UpdateCitizenshipCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCitizenshipCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCitizenshipCommand request, CancellationToken cancellationToken)
        {
            var citizenship = await _context.Citizenships.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (citizenship is null)
            {
                throw new NotFoundException(nameof(Citizenship), request.Id);
            }

            citizenship.NameRu = request.NameRu;
            citizenship.NameKg = request.NameKg;
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateCitizenshipCommandValidator : AbstractValidator<UpdateCitizenshipCommand>
    {
        public UpdateCitizenshipCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}