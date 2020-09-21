using StarMeApp.Application.Contracts.DTOs.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StarMeApp.Application.Contracts.DTOs
{
    public interface ITagDTO : IDTO<long>
    {
    }

    public abstract class TagDTO: ITagDTO
    {
        [DataMember]
        [Required]
        public long Id { get; set; }
        [DataMember]
        [Required]
        public string Name { get; set; }
    }

    public class GetTagDTO : TagDTO, IGetDTO<long>
    {
    }

    public class GetTagWithStoriesDTO : TagDTO, IGetDTO<long>
    {
        public GetTagWithStoriesDTO(): base()
        {
            this.Stories = new List<GetStoryDTO>();
        }

        [DataMember]
        public IEnumerable<GetStoryDTO> Stories { get; set; }
    }

    public class AddTagDTO : TagDTO, IAddDTO<long>
    {
    }
}
