using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectroInternalControlSystem.Models
{
    public class LoginContext : IdentityDbContext<ApplicationUser> //the 
    {
        public LoginContext(DbContextOptions<LoginContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
