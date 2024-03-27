using Domain.Dictionary;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
    ChangeTracker ChangeTracker { get; }

    EntityEntry<T> Entry<T>(T entity) where T : class;

    DbSet<ApplicationUser> Users { get; }
    DbSet<ApplicationUserRole> UserRoles { get; }

    DbSet<Candidate> Candidates { get; }
    DbSet<Person> People { get; }
    DbSet<Citizen> Citizens { get; }
    DbSet<Mother> Mothers { get; }
    DbSet<Foreigner> Foreigners { get; }
    DbSet<Entity> Entities { get; }
    
    DbSet<Domain.Entities.Application> RewardApplications { get; }
    
    DbSet<Office> Offices { get; }

    DbSet<Position> Positions { get; }
    DbSet<Citizenship> Citizenships { get; }
    DbSet<Reward> Rewards { get; }
    DbSet<Education> Educations { get; }
    DbSet<Member> Members { get; }
    DbSet<ApplicationStatus> RewardApplicationStatuses { get; }
    DbSet<PinAbsenceReason> PinAbsenceReasons { get; }
    DbSet<Document> Documents { get; }
    DbSet<DocumentType> DocumentTypes { get; } 
    DbSet<IssuedReward> IssuedRewards { get; } 
    DbSet<CandidateType> CandidateTypes { get; } 
    public DbSet<RewardDocumentType> RewardDocumentTypes { get; }

    DbSet<MotherChild> MotherChildren { get; } 
    DbSet<CandidateTypeReward> CandidateTypesRewards { get; }
    DbSet<UserOffice> UserOffices { get; }
    DbSet<ApplicationDocument> ApplicationDocuments { get; }
    DbSet<Child> Children { get; }
    DbSet<ChildDocument> ChildDocuments { get; }
    DbSet<ChildDocumentType> ChildDocumentTypes { get; }
    DbSet<Person> Persons { get; }
    DbSet<PinEntity> PinEntities { get; }
    public DbSet<PersonCandidate> PersonCandidates { get; }
    public DbSet<MemberType> MemberTypes { get; }
    public DbSet<Avatar> Avatars { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}