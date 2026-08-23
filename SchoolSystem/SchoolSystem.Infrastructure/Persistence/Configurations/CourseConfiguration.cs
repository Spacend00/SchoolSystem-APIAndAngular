
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Credit).IsRequired().HasColumnType("tinyint");
            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(c => c.CreatedAt).IsRequired();
            builder.Property(c => c.UpdatedAt).IsRequired(false);

            builder.HasOne(c => c.Teacher)
                .WithOne(t => t.Course)
                .HasForeignKey<Course>(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
