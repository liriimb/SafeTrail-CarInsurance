using CarInsuranceFinal.Models.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarInsuranceFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<FileCreation> Files { get; set; }
    }
}
