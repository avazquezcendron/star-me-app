using StarMeApp.Domain.Common;

namespace StarMeApp.Domain.BusinessEntities
{
    public class Tag : AbstractBusinessEntity<long>
    {
        public Tag()
        {
        }

        public string Name { get; set; }

    }
}
