using EducationSubscription.Core.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationBySubscription.Infrastructure.Persistence.Configurations.Entities.Payments;

public class PaymentConfiguration : BaseConfiguration<Payment>
{
    public override void Configure(EntityTypeBuilder<Payment> builder)
    {
        base.Configure(builder);
        builder.ToTable("tbl_Payments");
//        builder.HasOne(o => o.Subscription).WithMany().HasForeignKey(o => o.IdSubscription).IsRequired()
//            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(o => o.ExternalIdSubscription).IsRequired();
        builder.Property(o => o.Value).IsRequired();
        builder.Property(o => o.Value).HasPrecision(15, 4);
    }
}