using StarMeApp.Domain.Common;
using System.Collections.Generic;

namespace StarMeApp.Domain.BusinessEntities
{
    public class Story : AbstractBusinessEntity<long>, IAuditableBusinessEntity
    {
        public Story()
        {
            this.Tags = new List<Tag>();
            this.AuditInfo = new AuditInfoStruct();
        }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public AuditInfoStruct AuditInfo { get; set; }
    }
}
