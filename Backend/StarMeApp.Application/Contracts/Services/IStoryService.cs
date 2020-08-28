using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Domain.BusinessEntities;

namespace StarMeApp.Application.Contracts.Services
{
    public interface IStoryService: IGenericService<AddStoryDTO, GetStoryDTO, long>
    {

    }
}
