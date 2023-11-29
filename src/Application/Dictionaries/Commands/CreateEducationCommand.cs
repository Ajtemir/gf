using Application.Common.Dto;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Dictionary;
using FluentValidation;
using MediatR;

namespace Application.Dictionaries.Commands;

public class CreateEducationCommand : IRequest<EducationDto>
{
    public required string NameRu { get; set; }
    public required string NameKg { get; set; }
    
    public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, EducationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateEducationCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EducationDto> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = new Education() { NameRu = request.NameRu, NameKg = request.NameKg };
            
            _context.Educations.Add(education);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<EducationDto>(education);

            return dto;
        }
    }

    public class CreateEducationCommandValidator : AbstractValidator<CreateEducationCommand>
    {
        public CreateEducationCommandValidator()
        {
            RuleFor(x => x.NameRu).RequiredName();
            RuleFor(x => x.NameKg).RequiredName();
        }
    }
}