using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initializing the database: {Message}.", ex.Message);
            throw;
        }
    }

    public async Task InitializeRolesAndUsers()
    {
        try
        {
            await TryInitializeAuth();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initializing roles and users: {Message}.", ex.Message);
            throw;
        }
    }

    private async Task TryInitializeAuth()
    {
        var domainRoles = Enumeration.GetAll<DomainRole>();
        var existingRoles = _roleManager.Roles;

        var needToCreateRoles = domainRoles.ExceptBy(
            existingRoles.Select(er => er.Name),
            domainRole => domainRole.Name);

        foreach (var role in needToCreateRoles)
        {
            await _roleManager.CreateAsync(new ApplicationRole(role.Name, role.Note));
        }

        await CreateIfNotExistAdmin();
    }

    private async Task CreateIfNotExistAdmin()
    {
        var admin = await _userManager.FindByNameAsync("admin");
        if (admin is null)
        {
            var now = DateTime.UtcNow;
            admin = new ApplicationUser("admin", "John", "Doe", email: "admin@example.com")
            {
                CreatedBy = null,
                CreatedAt = now,
                ModifiedBy = null,
                ModifiedAt = now,
            };

            await _userManager.CreateAsync(admin, "Admin123!");
            await _userManager.AddToRolesAsync(admin, new[] { DomainRole.Administrator.Name });
        }
    }
}