using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    public class TagsController : GenericController<AddTagDTO, GetTagDTO, long>
    {

        public TagsController(ITagService tagService) : base(tagService)
        {
        }

    }
}
