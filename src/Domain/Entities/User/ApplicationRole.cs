using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName, string note) : base(roleName)
    {
        Note = note;
        ConcurrencyStamp = Guid.NewGuid().ToString();
    }

    public string Note { get; set; } = null!;

    public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}