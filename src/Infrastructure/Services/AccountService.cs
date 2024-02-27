using System.Security.Claims;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IApplicationDbContext _context;

    public AccountService(SignInManager<ApplicationUser> signInManager, IApplicationDbContext context)
    {
        _signInManager = signInManager;
        _context = context;
    }

    public async Task<ApplicationUser> Login(string userName, string password, CancellationToken cancellationToken = default)
    {
        var user = await _signInManager.UserManager.FindByNameAsync(userName);
        if (user is null)
        {
            throw new BadRequestException("Invalid credentials. Username not found");
        }

        var isValidPassword = await _signInManager.UserManager.CheckPasswordAsync(user, password);
        if (!isValidPassword)
        {
            throw new BadRequestException("Invalid credentials");
        }

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var userOffices = await _context.UserOffices.Where(x => x.UserId == user.Id).ToListAsync(cancellationToken: cancellationToken);
        userOffices.ForEach(x=>claims.Add(new Claim(ClaimTypes.Locality, x.OfficeId.ToString())));
        await _signInManager.SignInWithClaimsAsync(user,true, claims);
        
        return user;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}