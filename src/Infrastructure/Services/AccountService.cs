using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<ApplicationUser> Login(string userName, string password, CancellationToken cancellationToken = default)
    {
        var user = await _signInManager.UserManager.FindByNameAsync(userName);
        if (user is null)
        {
            throw new BadRequestException("Invalid credentials");
        }

        var isValidPassword = await _signInManager.UserManager.CheckPasswordAsync(user, password);
        if (!isValidPassword)
        {
            throw new BadRequestException("Invalid credentials");
        }

        await _signInManager.SignInAsync(user, isPersistent: true);
        
        return user;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}