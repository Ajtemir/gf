using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands;

public class UpdateUserImageCommand : ICommand
{
    public int Id { get; set; }
    public string? Image { get; set; }
    
    public class UpdateUserImageCommandHandler : IRequestHandler<UpdateUserImageCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeUtcService _dateTimeUtcService;

        public UpdateUserImageCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService, IDateTimeUtcService dateTimeUtcService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _dateTimeUtcService = dateTimeUtcService;
        }

        public async Task<Unit> Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId is 0)
            {
                throw new ForbiddenAccessException();
            }

            var image = Base64Helper.ConvertImageFromBase64(request.Image);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            user.Image = image;
            user.ModifiedBy = currentUserId;
            user.ModifiedAt = _dateTimeUtcService.Now;
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }

            return Unit.Value;
        }
    }


    public class UpdateUserImageCommandValidator : AbstractValidator<UpdateUserImageCommand>
    {
        public UpdateUserImageCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.Image).Image();
        }
    }
}