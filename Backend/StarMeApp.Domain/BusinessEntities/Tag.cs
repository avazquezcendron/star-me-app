using StarMeApp.Domain.Common;

namespace StarMeApp.Domain.BusinessEntities
{
    public class Tag : AuditableBusinessEntity
    {
        public Tag()
        {
        }

        public string Name { get; set; }

    }
}
