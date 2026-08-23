
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolSystem.Domain.Entities;

namespace SchoolSystem.Infrastructure.Persistence.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Surname).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Age).IsRequired().HasColumnType("tinyint");

            builder.Property(t => t.Email).IsRequired().HasMaxLength(256);
            builder.HasIndex(t => t.Email).IsUnique();

            builder.Property(t => t.PasswordHash).IsRequired().HasMaxLength(256);
            builder.Property(t => t.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(t => t.Role).IsRequired().HasConversion<string>();
            builder.Property(t => t.Branch).IsRequired().HasConversion<string>();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.UpdatedAt).IsRequired(false);

        }
    }
}
