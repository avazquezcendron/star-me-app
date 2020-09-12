using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarMeApp.Domain.BusinessEntities;

namespace StarMeApp.Infrastructure.Persistence.ModelConfigurations
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable("Stories");
            builder.HasKey(x => x.Id).IsClustered();            

            builder.Property(p => p.Title)
                .HasColumnType("varchar(130)")
                .HasMaxLength(130)
                .IsRequired();

            builder.Property(p => p.Summary)
                .HasColumnType("varchar(200)")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Content)
                .HasColumnType("text")
                .IsRequired();

            builder.OwnsOne(x => x.AuditInfo)
                .Property(y => y.CreatedAt)
                .HasColumnType("datetime2")
                .ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.AuditInfo)
               .Property(y => y.UpdatedAt)
               .HasColumnType("datetime2")
                .ValueGeneratedOnUpdate();

            builder.OwnsOne(x => x.AuditInfo)
                .Property(y => y.State)
                .HasColumnType("char(1)")
                .HasMaxLength(1);

            builder.OwnsOne(x => x.AuditInfo)
                .Property(y => y.User)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30);
        }
    }
}
