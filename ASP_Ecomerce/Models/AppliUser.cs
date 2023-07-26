using Microsoft.AspNetCore.Identity;

namespace ASP_Ecomerce.Models
{
    public class AppliUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
