
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
