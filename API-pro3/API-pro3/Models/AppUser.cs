using Microsoft.AspNetCore.Identity;

namespace API_pro3.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
    }
}
