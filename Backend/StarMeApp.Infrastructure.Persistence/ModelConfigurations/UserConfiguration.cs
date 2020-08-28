using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarMeApp.Domain.BusinessEntities;

namespace StarMeApp.Infrastructure.Persistence.ModelConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Username)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Password)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("varchar(50)")                
                .HasMaxLength(50);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasIndex(x => x.Username).IsUnique();


        }
    }
}
