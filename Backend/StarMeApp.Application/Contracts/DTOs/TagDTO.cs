using StarMeApp.Application.Contracts.DTOs.Common;

namespace StarMeApp.Application.Contracts.DTOs
{
    public interface ITagDTO : IDTO<long>
    {
    }

    public abstract class TagDTO: ITagDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class GetTagDTO : TagDTO, IGetDTO<long>
    {
    }

    public class AddTagDTO : TagDTO, IAddDTO<long>
    {
    }
}
