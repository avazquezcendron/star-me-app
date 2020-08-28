using StarMeApp.Domain.Common;
using System.Collections.Generic;

namespace StarMeApp.Domain.BusinessEntities
{
    public class Story : AbstractBusinessEntity<long>, IAuditableBusinessEntity
    {
        public Story()
        {
            this.Tags = new HashSet<StoryTags>();

            this.AuditInfo = new AuditInfoStruct();
        }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public ICollection<StoryTags> Tags { get; set; }
        public AuditInfoStruct AuditInfo { get; set; }

        public void AddTag(Tag tag)
        {
            this.Tags.Add(new StoryTags() { Story = this, StoryId = this.Id, Tag = tag, TagId = tag.Id });
        }

        public void ClearTags()
        {
            this.Tags = new HashSet<StoryTags>();

        }
    }
}
