using Microsoft.AspNetCore.Identity;

namespace diplom.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool isActivity { get; set; }
        public string? Location { get; set; }
    }
}
