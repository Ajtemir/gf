using Application.Common.Dto;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Dictionary;
using FluentValidation;
using MediatR;

namespace Application.Dictionaries.Commands;

public class CreatePositionCommand : IRequest<PositionDto>
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, PositionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreatePositionCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = new Position { NameRu = request.NameRu, NameKg = request.NameKg };
            
            _context.Positions.Add(position);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<PositionDto>(position);
            
            return dto;
        }
    }

    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionCommandValidator()
        {
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}