using Microsoft.EntityFrameworkCore;
using StudentsPortal.web.Models.Entities;
namespace StudentsPortal.web.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal object Students;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Students> Student { get; set; }
    }
}
