using EducationBySubscription.Infrastructure.Persistence.Common;
using EducationSubscription.Core.Domain.Courses;
using EducationSubscription.Core.Domain.Members;
using EducationSubscription.Core.Domain.Payments;
using EducationSubscription.Core.Domain.Plans;
using EducationSubscription.Core.Domain.Subscriptions;
using EducationSubscription.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EducationBySubscription.Infrastructure.Persistence;

/// <summary>
/// Represents a context to access the database.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Base constructor.
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions options) : base(options) { }

    
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Plan> Plans { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}