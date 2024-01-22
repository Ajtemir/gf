using Application.Common.Dto;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Application.Account.Commands;

public class LoginCommand : IRequest<UserDto>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDto>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public LoginCommandHandler(IAccountService accountService, IMapper mapper, IApplicationDbContext context)
        {
            _accountService = accountService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountService.Login(request.UserName, request.Password, cancellationToken);
            var dto = _mapper.Map<UserDto>(user);

            var relatedData = await _context.Users
                .Where(x => x.Id == user.Id)
                .Select(u => new
                {
                    Roles = u.UserRoles.Select(userRole => userRole.Role.Name!),
                    Offices = u.UserOffices.Select(userOffice => new UserOfficeDto()
                    {
                        OfficeId = userOffice.OfficeId,
                        NameRu = userOffice.Office!.NameRu,
                        NameKg = userOffice.Office!.NameKg,
                    }),
                })
                .FirstAsync(cancellationToken);
            
            dto.Roles = relatedData.Roles;
            dto.Offices = relatedData.Offices;

            return dto;
        }
    }
    
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).UserName();
            RuleFor(x => x.Password).Password();
        }
    }
}