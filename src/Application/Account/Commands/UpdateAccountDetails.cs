using Application.Common.Dto;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Validators;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Account.Commands;

public class UpdateAccountDetailsCommand : IRequest<UserDto>
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? PatronymicName { get; set; }
    public string? Pin { get; set; }
    public string? Email { get; set; }
    
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateAccountDetailsCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UpdateUserDetailsCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateAccountDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Id);
            }

            user.UserName = request.UserName;
            user.LastName = request.LastName;
            user.FirstName = request.FirstName;
            user.PatronymicName = request.PatronymicName;
            user.Pin = request.Pin;
            user.Email = request.Email;
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException(result.ToErrorMessage());
            }

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
    
    public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateAccountDetailsCommand>
    {
        public UpdateUserDetailsCommandValidator()
        {
            RuleFor(x => x.Id).Id();
            RuleFor(x => x.UserName).UserName();
            RuleFor(x => x.LastName).LastName();
            RuleFor(x => x.FirstName).FirstName();
            RuleFor(x => x.PatronymicName).PatronymicName();
            RuleFor(x => x.Pin).Pin();
            RuleFor(x => x.Email).Email();
        }
    }
}