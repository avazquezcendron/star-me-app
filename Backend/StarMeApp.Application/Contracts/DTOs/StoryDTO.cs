using StarMeApp.Application.Contracts.DTOs.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StarMeApp.Application.Contracts.DTOs
{
    
    public abstract class StoryDTO
    {
        [DataMember]
        [Required]
        public long Id { get; set; }
        [DataMember]
        [Required]
        public string Title { get; set; }
        [DataMember]
        [Required]
        public string Summary { get; set; }
        [DataMember]
        [Required]
        public string Content { get; set; }
    }

    public class GetStoryDTO : StoryDTO, IGetDTO<long>, IAuditableDTO
    {
        public GetStoryDTO(): base()
        {
            this.AuditInfo = new AuditInfoStructDTO();
            this.Tags = new List<GetTagDTO>();

        }
        [DataMember]
        public AuditInfoStructDTO AuditInfo { get; set; }
        [DataMember]
        public IEnumerable<GetTagDTO> Tags { get; set; }
    }

    public class AddStoryDTO : StoryDTO, IAddDTO<long>
    {
        public AddStoryDTO()
        {
            this.Tags = new List<AddTagDTO>();
        }
        [DataMember]
        public IEnumerable<AddTagDTO> Tags { get; set; }

    }
}
