using EducationSubscription.Core.Domain.Members;
using EducationSubscription.Core.Domain.Plans;
using EducationSubscription.Core.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationBySubscription.Infrastructure.Persistence.Configurations.Entities.Subscriptions;

public class SubscriptionConfiguration : BaseConfiguration<Subscription>
{
    public override void Configure(EntityTypeBuilder<Subscription> builder)
    {
        base.Configure(builder);
        builder.ToTable("tbl_Subscriptions");
        builder.HasOne<Plan>(o => o.Plan).WithMany().HasForeignKey(o => o.IdPlan).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Member>(o => o.Member).WithMany().HasForeignKey(o => o.IdMember)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
