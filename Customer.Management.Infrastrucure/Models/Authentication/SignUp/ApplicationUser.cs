using Microsoft.AspNetCore.Identity;

namespace Customer.Management.API.Models.Authentication.SignUp
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
