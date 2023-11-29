using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Entities.Commands;
public class UpdateEntityCommand : ICommand
{
    public int Id { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class UpdateEntityCommandHandler : IRequestHandler<UpdateEntityCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEntityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Entities.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Entity), request.Id);
            }

            entity.NameRu = request.NameRu;
            entity.NameKg = request.NameKg;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    public class UpdateEntityCommandValidator : AbstractValidator<UpdateEntityCommand>
    {
        public UpdateEntityCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}
