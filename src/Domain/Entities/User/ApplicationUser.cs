using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class ApplicationUser : IdentityUser<int>, ISoftDeleteEntity
{
    public ApplicationUser()
    {
        // EF Core needs this constructor even though it is never called by 
        // my code in the application. EF Core needs it to set up the contexts

        // Failure to have it will result in a 
        // No suitable constructor found for entity type 'User'. exception
    }

    public ApplicationUser(
        string userName,
        string lastName,
        string firstName,
        string? patronymicName = null,
        string? email = null,
        string? pin = null)
        : base(userName)
    {
        LastName = lastName;
        FirstName = firstName;
        PatronymicName = patronymicName;
        Email = email;
        Pin = pin;

        SecurityStamp = Guid.NewGuid().ToString();
    }

    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? PatronymicName { get; set; }
    public string? Pin { get; set; }

    /// <summary>
    /// The only reason it is nullable - for easier tests.
    /// When Respawner wipes all data, including users,
    /// creating new user with non-nullable <see cref="CreatedBy"/> is difficult.
    /// </summary>
    public int? CreatedBy { get; set; }
    public ApplicationUser? CreatedByUser { get; set; }
    public required DateTime CreatedAt { get; set; }
    

    /// <summary>
    /// Same as <see cref="CreatedBy"/>.
    /// </summary>
    public int? ModifiedBy { get; set; }
    public ApplicationUser? ModifiedByUser { get; set; }
    public required DateTime ModifiedAt { get; set; }
    
    public byte[]? Image { get; set; }
    
    public bool IsDeleted { get; set; }

    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    public ICollection<Office> Offices { get; set; } = new List<Office>();
    public ICollection<UserOffice> UserOffices { get; set; } = new List<UserOffice>();
    
    public ICollection<ApplicationUser> CreatedUsers { get; set; } = new List<ApplicationUser>();
    public ICollection<ApplicationUser> ModifiedUsers { get; set; } = new List<ApplicationUser>();
    public ICollection<ApplicationStatus> Statuses { get; set; } = new List<ApplicationStatus>();
}