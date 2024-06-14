using EducationSubscription.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationBySubscription.Infrastructure.Persistence.Configurations.Entities.Users;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("tbl_Users");
        builder.Property(o => o.Email).IsRequired().HasMaxLength(100);
        builder.Property(o => o.Password).IsRequired().HasMaxLength(260);
    }
}