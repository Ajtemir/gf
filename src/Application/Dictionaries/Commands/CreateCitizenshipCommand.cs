using Application.Common.Dto;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Dictionary;
using FluentValidation;
using MediatR;

namespace Application.Dictionaries.Commands;

public class CreateCitizenshipCommand : IRequest<CitizenshipDto>
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }

    public class CreateCitizenshipCommandHandler : IRequestHandler<CreateCitizenshipCommand, CitizenshipDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCitizenshipCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitizenshipDto> Handle(CreateCitizenshipCommand request, CancellationToken cancellationToken)
        {
            var citizenship = new Citizenship() { NameRu = request.NameRu, NameKg = request.NameKg };

            _context.Citizenships.Add(citizenship);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<CitizenshipDto>(citizenship);

            return dto;
        }
    }


    public class CreateCitizenshipCommandValidator : AbstractValidator<CreateCitizenshipCommand>
    {
        public CreateCitizenshipCommandValidator()
        {
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}