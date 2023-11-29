using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Users.Commands;

public class DeleteUserCommand : ICommand
{
    public int Id { get; set; }
    
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService, ILogger<DeleteUserCommandHandler> logger)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            if (currentUserId is 0)
            {
                throw new ForbiddenAccessException();
            }
            
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }
            
            _logger.LogInformation("""
                The user with id {UserId} was deleted by the user with id {CurrentUserId}
                User: {@User}
                """, 
                user.Id, _currentUserService.UserId, user);

            return Unit.Value;
        }
    }

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Id).Id();
        }
    }
}