using FinalCIS174.Models.UserLogin;
using Microsoft.AspNetCore.Identity;

namespace FinalCIS174.Areas.Admin.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
