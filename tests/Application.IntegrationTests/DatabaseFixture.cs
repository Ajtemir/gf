using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Npgsql;
using Respawn;
using Respawn.Graph;

namespace Application.IntegrationTests;

[CollectionDefinition(nameof(DatabaseCollection))]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}

public sealed class DatabaseFixture : IAsyncLifetime, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _connectionString;
    private Respawner _respawner = null!;
    private NpgsqlConnection _connection = null!;
    private int _currentUserId;
    private readonly IMapper _mapper;

    public DatabaseFixture()
    {
        var factory = new TestingWebApplicationFactory(services =>
            services.AddScoped(_ => Mock.Of<ICurrentUserService>(s => s.UserId == GetCurrentUserId())));

        _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        
        var configuration = factory.Services.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not bind the connection string 'DefaultConnection'");
        }

        _connectionString = connectionString;

        _mapper = factory.Services.GetRequiredService<IMapper>();
    }

    public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }
    
    public async Task SendAsync(IRequest request)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        await mediator.Send(request);
    }
    
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }
    
    public async Task<int> RunAsAdministrator()
    {
        return await RunAsUserAsync("admin", "Admin123!", new[] { DomainRole.Administrator });
    } 
    
    private async Task<int> RunAsUserAsync(string username, string password, DomainRole[] roles)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var existingUser = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        if (existingUser is not null)
        {
            _currentUserId = existingUser.Id;
            return _currentUserId;
        }

        var now = DateTime.UtcNow;
        var user = new ApplicationUser(username, username, username, username, email: $"{username}@test.com", pin: "20101197012345")
        {
            CreatedBy = null,
            CreatedAt = now,
            ModifiedBy = null,
            ModifiedAt = now
        };
        
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
            throw new ArgumentException(errors);
        }

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new ApplicationRole(role.Name, role.Note));
            }

            await userManager.AddToRolesAsync(user, roles.Select(r => r.Name));
        }

        _currentUserId = user.Id;
        return _currentUserId;
    }

    public void ResetCurrentUserId()
    {
        _currentUserId = 0;
    }
    
    public HttpContext GetHttpContextWithServices()
    {
        var scope = _scopeFactory.CreateScope();
        var defaultHttpContext = new DefaultHttpContext() { RequestServices = scope.ServiceProvider };
        return defaultHttpContext;
    }

    public ControllerContext GetControllerContextWithServices() => new() { HttpContext = GetHttpContextWithServices() };
    
    /// <summary>
    /// Runs once before any tests. Creates a database if needed and opens DB connection.
    /// </summary>
    public async Task InitializeAsync()
    {
        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync();
        
        _respawner = await Respawner.CreateAsync(_connection,
            new RespawnerOptions()
            {
                TablesToIgnore = new Table[] { "__EFMigrationsHistory", },
                SchemasToExclude = new [] { "public" },
                SchemasToInclude = new[] { "main" },
                DbAdapter = DbAdapter.Postgres
            }
        );
    }

    /// <summary>
    /// Runs once after all tests, removing all data from database.
    /// </summary>
    public async Task DisposeAsync() => await ResetState();

    /// <summary>
    /// Wipes all data from database.
    /// </summary>
    public async Task ResetState()
    {
        _currentUserId = 0;
        await _respawner.ResetAsync(_connection);
    }

    public void Dispose() => _connection.Dispose();

    private int GetCurrentUserId() => _currentUserId;
}