using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Web.Data;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime MembershipDate { get; set; } = DateTime.Now;
}
