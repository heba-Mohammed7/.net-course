using Microsoft.AspNetCore.Identity;

namespace authentication.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsEmailVerified { get; set; }
}