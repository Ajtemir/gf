using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Helpers;
using Application.Common.Validators;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands;

public class UpdateAccountImageCommand : IRequest<string?>
{
    public int Id { get; set; }
    public string? Image { get; set; }
    
    public class UpdateAccountImageCommandHandler : IRequestHandler<UpdateAccountImageCommand, string?>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateAccountImageCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string?> Handle(UpdateAccountImageCommand request, CancellationToken cancellationToken)
        {
            var image = Base64Helper.ConvertImageFromBase64(request.Image);
            
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }
            
            user.Image = image;
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }

            if (image is null)
            {
                return null;
            }

            return Convert.ToBase64String(image);
        }
    }

    public class UpdateAccountImageCommandValidator : AbstractValidator<UpdateAccountImageCommand>
    {
        public UpdateAccountImageCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.Image).Image();
        }
    }
}