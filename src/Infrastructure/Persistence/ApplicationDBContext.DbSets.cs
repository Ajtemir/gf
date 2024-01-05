using Domain.Dictionary;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class ApplicationDbContext
{
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
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<RewardApplicationStatus> RewardApplicationStatuses { get; set; } = null!;
}