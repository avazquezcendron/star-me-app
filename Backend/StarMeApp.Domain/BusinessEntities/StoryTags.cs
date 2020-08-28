using StarMeApp.Domain.Common;
using System;

namespace StarMeApp.Domain.BusinessEntities
{
    public class StoryTagsId
    {
        public long TagId { get; set; }
        public long StoryId { get; set; }
    }

    public class StoryTags
    {
        public StoryTags()
        {
        }

        public long TagId { get; set; }
        public long StoryId { get; set; }


        public Story Story { get; set; }
        public Tag Tag { get; set; }

    }
}
