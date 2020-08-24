using System;

namespace StarMeApp.Domain.Common
{
    public class AuditableBusinessEntity
    {
        public virtual long Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Status { get; set; }
        public string User { get; set; }
    }
}
