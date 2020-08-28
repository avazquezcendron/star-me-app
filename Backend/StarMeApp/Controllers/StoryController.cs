using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    public class StoryController : GenericController<AddStoryDTO, GetStoryDTO, long>
    {

        public StoryController(IStoryService storyService): base(storyService)
        {
        }

    }
}
