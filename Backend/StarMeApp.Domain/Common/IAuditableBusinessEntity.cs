using System;

namespace StarMeApp.Domain.Common
{
    public interface IAuditableBusinessEntity
    {
        AuditInfoStruct AuditInfo { get; set; }
    }

    public struct AuditInfoStruct
    {      
        public DateTime CreatedAt { get; set; }
        
        public string State { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        public string User { get; set; }
    }
}
