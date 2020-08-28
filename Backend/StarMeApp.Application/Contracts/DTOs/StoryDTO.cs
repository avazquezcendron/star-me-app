using StarMeApp.Application.Contracts.DTOs.Common;
using System.Collections.Generic;

namespace StarMeApp.Application.Contracts.DTOs
{
    
    public abstract class StoryDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
    }

    public class GetStoryDTO : StoryDTO, IGetDTO<long>, IAuditableDTO
    {
        public GetStoryDTO(): base()
        {
            this.AuditInfo = new AuditInfoStructDTO();
            this.Tags = new List<GetTagDTO>();

        }
        public AuditInfoStructDTO AuditInfo { get; set; }
        public IEnumerable<GetTagDTO> Tags { get; set; }
    }

    public class AddStoryDTO : StoryDTO, IAddDTO<long>
    {
        public AddStoryDTO()
        {
            this.Tags = new List<AddTagDTO>();
        }

        public IEnumerable<AddTagDTO> Tags { get; set; }

    }
}
