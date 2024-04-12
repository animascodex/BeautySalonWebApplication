using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeautySalonWebApplication.Models;

namespace BeautySalonWebApplication.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override DbSet<ApplicationUser> Users {  get; set; }
        public DbSet<BeautySalonWebApplication.Models.Appointment> Appointment { get; set; } = default!;
    }
}
