using AutoMapper;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;

namespace StarMeApp.Infrastructure.Persistence.Services
{
    public class StoryService : GenericService<StoryDTO, Story, long, long>, IStoryService
    {

        public StoryService(IMapper mapper, IStoryRepositoryAsync storyRepository): base (mapper, storyRepository)
        {
        }

    }
}
