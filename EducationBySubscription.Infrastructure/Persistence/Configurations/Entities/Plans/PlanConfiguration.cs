using EducationSubscription.Core.Domain.Plans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationBySubscription.Infrastructure.Persistence.Configurations.Entities.Plans;

public class PlanConfiguration : BaseConfiguration<Plan>
{
    public override void Configure(EntityTypeBuilder<Plan> builder)
    {
        base.Configure(builder);
        builder.ToTable("tbl_Plans");
        builder.Property(o => o.Title).HasMaxLength(100).IsRequired();
        builder.Property(o => o.Description).HasMaxLength(500).IsRequired();
        builder.Property(o => o.BasePrice).HasPrecision(10, 4);
        builder.Property(o => o.AllowedCourses).IsRequired();
    }
}