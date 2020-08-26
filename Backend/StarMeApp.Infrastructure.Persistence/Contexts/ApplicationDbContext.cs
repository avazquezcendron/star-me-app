using Microsoft.EntityFrameworkCore;
using StarMeApp.Domain.BusinessEntities;
using StarMeApp.Domain.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarMeApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        //private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableBusinessEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AuditInfo = new AuditInfoStruct()
                        {
                            CreatedAt = DateTime.UtcNow,
                            State = "I"
                        };                    
                        break;
                    case EntityState.Modified:
                        var prevCreatedAt = entry.Entity.AuditInfo.CreatedAt;
                        entry.Entity.AuditInfo = new AuditInfoStruct()
                        {
                            UpdatedAt = DateTime.UtcNow,
                            CreatedAt = prevCreatedAt,
                            State = "U"
                        };
                        //entry.Entity.User = _authenticatedUser.UserId;
                        break;
                    case EntityState.Deleted:
                        var _prevCreatedAt = entry.Entity.AuditInfo.CreatedAt;                        
                        entry.Entity.AuditInfo = new AuditInfoStruct()
                        {
                            UpdatedAt = DateTime.UtcNow,
                            CreatedAt = _prevCreatedAt,
                            State = "D"
                        };
                        //entry.Entity.User = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    //All Decimals will have 18,6 Range
        //    foreach (var property in builder.Model.GetEntityTypes()
        //    .SelectMany(t => t.GetProperties())
        //    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        //    {
        //        property.("decimal(18,6)");
        //    }
        //    base.OnModelCreating(builder);
        //}
    }
}
