using Junior_Student_Portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Junior_Student_Portal.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        { 
        }
        public DbSet<Student> Students { get; set; }
    }
}
