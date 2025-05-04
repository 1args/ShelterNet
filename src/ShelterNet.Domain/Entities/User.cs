using ShelterNet.Domain.Primitives;

namespace ShelterNet.Domain.Entities;

/// <summary>
/// Entity representing a system user
/// </summary>
public class User : Entity<Guid>
{
    /// <summary>User's full name</summary>
    public string FullName { get; set; }

    /// <summary>User's email address</summary>
    public string Email { get; set; }

    /// <summary>Hashed password</summary>
    public string PasswordHash { get; set; }

    /// <summary>User roles in the system</summary>
    public ICollection<Role> Roles { get; set; }

    public User() { }
}