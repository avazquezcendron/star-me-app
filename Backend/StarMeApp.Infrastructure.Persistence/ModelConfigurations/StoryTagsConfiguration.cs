using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarMeApp.Domain.BusinessEntities;

namespace StarMeApp.Infrastructure.Persistence.ModelConfigurations
{
    public class StoryTagsConfiguration : IEntityTypeConfiguration<StoryTags>
    {
        public void Configure(EntityTypeBuilder<StoryTags> builder)
        {
            builder.ToTable("StoryTags");
            
            builder.HasKey(ts => new { ts.StoryId, ts.TagId });

            builder
                .HasOne(ts => ts.Story)
                .WithMany(s => s.Tags)
                .HasForeignKey(ts => ts.StoryId);

            builder
                .HasOne(ts => ts.Tag)
                .WithMany(s => s.Stories)
                .HasForeignKey(ts => ts.TagId);
        }
    }
}
