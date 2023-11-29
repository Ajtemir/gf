using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Commands;

public class CreateUserCommand : IRequest<UserDto>
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public string? Email { get; set; }
    public string? Pin { get; set; }
    public string? Image { get; set; }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeUtcService _dateTimeUtcService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(ICurrentUserService currentUserService, IDateTimeUtcService dateTimeUtcService,
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _dateTimeUtcService = dateTimeUtcService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == 0)
            {
                throw new ForbiddenAccessException();
            }

            var now = _dateTimeUtcService.Now;
            var image = Base64Helper.ConvertImageFromBase64(request.Image);


            var user = new ApplicationUser(
                request.UserName,
                request.LastName,
                request.FirstName,
                request.PatronymicName,
                email: request.Email,
                pin: request.Pin)
            {
                CreatedBy = userId,
                CreatedAt = now,
                ModifiedBy = userId,
                ModifiedAt = now,
                Image = image,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }

            if (user.CreatedBy != user.ModifiedBy)
            {
                throw new InvalidCreatedModifiedValuesException($"""
                    An {nameof(ApplicationUser)} with id {user.Id} was created, but fields '{nameof(ApplicationUser.CreatedBy)}', '{nameof(ApplicationUser.ModifiedBy)}' do not have matching values.
                    '{nameof(ApplicationUser.CreatedBy)}' : {user.CreatedBy}
                    '{nameof(ApplicationUser.ModifiedBy)}' : {user.ModifiedBy}
                    """);
            }

            var dto = _mapper.Map<UserDto>(user);

            return dto;
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName).UserName();
            RuleFor(x => x.Password).NotEmpty().Password();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.PatronymicName).PatronymicName();
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Pin).Pin();
            RuleFor(x => x.Image).Image();
        }
    }
}