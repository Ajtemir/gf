using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Application.Offices.Commands;

public class UpdateOfficeCommand : ICommand
{
    public required int Id { get; set; }
    public required int NewId { get; set; }
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateOfficeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            var exists = _context.Offices.Any(x => x.Id == request.Id);
            if (!exists)
            {
                throw new NotFoundException(nameof(Office), request.Id);
            }

            var rowsAffected = await _context.Database.ExecuteSqlAsync($"""
                UPDATE main.offices
                SET Id = {request.NewId}, name_ru = {request.NameRu}, name_kg = {request.NameKg}
                WHERE Id = {request.Id} 
                """, cancellationToken);

            if (rowsAffected == 0)
            {
                throw new InternalServerException($"Не получилось обновить {nameof(Office)} с {nameof(Office.Id)}: {request.Id}");
            }

            return Unit.Value;
        }
    }

    public class UpdateOfficeCommandValidator : AbstractValidator<UpdateOfficeCommand>
    {
        public UpdateOfficeCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NewId).Id();
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}
