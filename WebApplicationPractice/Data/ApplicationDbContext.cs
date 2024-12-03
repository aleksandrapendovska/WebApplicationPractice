using Microsoft.EntityFrameworkCore;

namespace WebApplicationPractice.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.User> Users { get; set; }
    }
}
