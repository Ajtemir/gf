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

public class UpdateUserPasswordCommand : ICommand
{
    public int Id { get; set; }
    public required string NewPassword { get; set; }

    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeUtcService _dateTimeUtcService;

        public UpdateUserPasswordCommandHandler(UserManager<ApplicationUser> userManager,
            ICurrentUserService currentUserService, IDateTimeUtcService dateTimeUtcService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _dateTimeUtcService = dateTimeUtcService;
        }

        public async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId is 0)
            {
                throw new ForbiddenAccessException();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }

            user.ModifiedBy = currentUserId;
            user.ModifiedAt = _dateTimeUtcService.Now;
            
            result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }

            return Unit.Value;
        }
    }

    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.NewPassword).Password();
        }
    }
}