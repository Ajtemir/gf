using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Dictionary;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dictionaries.Commands;

public class UpdateEducationCommand : ICommand
{
    public required int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEducationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _context.Educations.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (education is null)
            {
                throw new NotFoundException(nameof(Education), request.Id);
            }

            education.NameRu = request.NameRu;
            education.NameKg = request.NameKg;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
    {
        public UpdateEducationCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}