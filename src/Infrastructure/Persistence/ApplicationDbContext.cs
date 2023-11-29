using System.Reflection;
using Application.Common.Interfaces;
using Domain.Dictionary;
using Domain.Entities;
using Infrastructure.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        IdentityUserClaim<int>,
        ApplicationUserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>,
    IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<Candidate> Candidates { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Citizen> Citizens { get; set; } = null!;
    public DbSet<Mother> Mothers { get; set; } = null!;
    public DbSet<Foreigner> Foreigners { get; set; } = null!;
    public DbSet<Entity> Entities { get; set; } = null!;

    public DbSet<RewardApplication> RewardApplications { get; set; } = null!;
    public DbSet<Office> Offices { get; set; } = null!;

    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Citizenship> Citizenships { get; set; } = null!;
    public DbSet<Reward> Rewards { get; set; } = null!;
    public DbSet<Education> Educations { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("main");
        
        base.OnModelCreating(builder);
        
        // Add the Postgres Extension for UUID generation
        builder.HasPostgresExtension("uuid-ossp");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Conventions.Add(_ => new StringPropertyMaxLengthConvention(128));
        configurationBuilder.Conventions.Add(_ => new SnakeNamingConvention());
        configurationBuilder.Conventions.Add(_ => new SoftDeleteConvention());
    }
}