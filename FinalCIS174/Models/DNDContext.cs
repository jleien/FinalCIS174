using FinalCIS174.Models.UserLogin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalCIS174.Models
{
    public class DNDContext : IdentityDbContext<User>
    {
        public DNDContext(DbContextOptions<DNDContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
