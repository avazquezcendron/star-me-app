using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    public class TagController : GenericController<AddTagDTO, GetTagDTO, long>
    {

        public TagController(ITagService tagService) : base(tagService)
        {
        }

    }
}
