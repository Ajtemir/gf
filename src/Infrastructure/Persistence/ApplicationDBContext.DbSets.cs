using Domain.Dictionary;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class ApplicationDbContext
{
    public DbSet<ApplicationUserRole> UserRoles { get; set; } = null!;
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
    public DbSet<PinAbsenceReason> PinAbsenceReasons { get; set; } = null!;
    public DbSet<Document> Documents { get; set; } = null!;
    public DbSet<DocumentType> DocumentTypes { get; set; } = null!;
    public DbSet<IssuedReward> IssuedRewards { get; set; } = null!;
    public DbSet<CandidateType> CandidateTypes { get; set; } = null!;
    public DbSet<RewardDocumentType> RewardDocumentTypes { get; set; } = null!;
    public DbSet<MotherChild> MotherChildren { get; set; } = null!;
    public DbSet<CandidateTypeReward> CandidateTypesRewards { get; set; } = null!;
    public DbSet<UserOffice> UserOffices { get; set; } = null!;
    public DbSet<ApplicationDocument> ApplicationDocuments { get; }
    public DbSet<Child> Children { get; }
    public DbSet<ChildDocument> ChildDocuments { get; }
    public DbSet<ChildDocumentType> ChildDocumentTypes { get; }
}