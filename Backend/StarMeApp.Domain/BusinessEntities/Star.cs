using StarMeApp.Domain.Common;
using System.Collections.Generic;

namespace StarMeApp.Domain.BusinessEntities
{
    public class Star : AuditableBusinessEntity
    {
        public Star()
        {
            this.Tags = new List<Tag>();
        }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
