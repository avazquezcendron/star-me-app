using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Domain.BusinessEntities;
using System.Threading.Tasks;

namespace StarMeApp.Application.Contracts.Services
{
    public interface IStoryService: IGenericService<AddStoryDTO, GetStoryDTO, long>
    {
        Task<ResponseListDTO<GetStoryDTO>> GetStoriesByTag(long tagId);
        Task<ResponseListDTO<GetStoryDTO>> GetStoriesByTitle(string title);
    }
}
