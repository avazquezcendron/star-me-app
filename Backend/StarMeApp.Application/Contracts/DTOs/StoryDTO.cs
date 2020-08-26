using StarMeApp.Application.Contracts.DTOs.Common;
using System.Collections.Generic;

namespace StarMeApp.Application.Contracts.DTOs
{
    public class StoryDTO : IDTO<long>, IAuditableDTO
    {
        public StoryDTO()
        {
            this.AuditInfo = new AuditInfoStructDTO();
            this.Tags = new List<TagDTO>();
        }


        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }
        public AuditInfoStructDTO AuditInfo { get; set; }
    }
}
