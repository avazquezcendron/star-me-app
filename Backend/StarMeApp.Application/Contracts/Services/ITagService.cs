using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.DTOs.Common;
using System.Threading.Tasks;

namespace StarMeApp.Application.Contracts.Services
{
    public interface ITagService : IGenericService<AddTagDTO, GetTagDTO, long>
    {
        Task<ResponseValueDTO<GetTagWithStoriesDTO>> GetTagWithStoriesAsync(long tagId);
    }
}
