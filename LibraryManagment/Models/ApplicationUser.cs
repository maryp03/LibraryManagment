using Microsoft.AspNetCore.Identity;

namespace LibraryManagment.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
