
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Infrastructure.Persistence.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.ToTable("StudentCourses");
            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(sc => sc.EnrolledAt).IsRequired();
            builder.Property(sc => sc.UnEnrolledAt).IsRequired(false);

            builder.HasIndex(sc => new { sc.StudentId, sc.CourseId }).IsUnique();

            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Course)
                .WithMany()
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
