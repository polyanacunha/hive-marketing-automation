using Hive.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hive.Infra.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAtUtc { get; set; }
    public virtual ClientProfile? ClientProfile { get; set; }
}
