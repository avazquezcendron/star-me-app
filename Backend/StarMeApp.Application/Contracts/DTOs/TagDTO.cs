using StarMeApp.Application.Contracts.DTOs.Common;
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

    public class AddTagDTO : TagDTO, IAddDTO<long>
    {
    }
}
