
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Surname).IsRequired().HasMaxLength(50);
            builder.Property(s => s.Age).IsRequired().HasColumnType("tinyint");
            builder.Property(s => s.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(s => s.TotalCredit).IsRequired().HasColumnType("tinyint");
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.UpdatedAt).IsRequired(false);

            builder.Property(s => s.SchoolNumber).IsRequired().HasMaxLength(20);
            builder.HasIndex(s => s.SchoolNumber).IsUnique();

            builder.Property(s => s.Email).IsRequired().HasMaxLength(256);
            builder.HasIndex(s => s.Email).IsUnique();

            builder.Property(s => s.PasswordHash).IsRequired().HasMaxLength(256);

            builder.Property(s => s.Role).HasConversion<string>().HasMaxLength(20).IsRequired();
        }
    }
}
