using EducationSubscription.Core.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationBySubscription.Infrastructure.Persistence.Configurations.Entities.Courses;

public class CourseConfiguration : BaseConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);
        builder.ToTable("tbl_Courses");
        builder.HasMany(o => o.Lessons).WithOne(o => o.Course).HasForeignKey(o => o.IdCourse).IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.Description).IsRequired();
        builder.Property(o => o.Cover).IsRequired(false);
    }
}