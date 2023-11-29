using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands;

public class UpdateUserDetailsCommand : ICommand
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public string? Email { get; set; }
    public string? Pin { get; set; }

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, Unit>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeUtcService _dateTimeUtcService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserDetailsCommandHandler(ICurrentUserService currentUserService,
            IDateTimeUtcService dateTimeUtcService, UserManager<ApplicationUser> userManager)
        {
            _currentUserService = currentUserService;
            _dateTimeUtcService = dateTimeUtcService;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId is 0)
            {
                throw new ForbiddenAccessException();
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            user.UserName = request.UserName;
            user.LastName = request.LastName;
            user.FirstName = request.FirstName;
            user.PatronymicName = request.PatronymicName;
            user.Email = request.Email;
            user.Pin = request.Pin;
            user.ModifiedBy = userId;
            user.ModifiedAt = _dateTimeUtcService.Now;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }

            return Unit.Value;
        }
    }

    public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        public UpdateUserDetailsCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.UserName).UserName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.PatronymicName).PatronymicName();
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Pin).Pin();
        }
    }
}