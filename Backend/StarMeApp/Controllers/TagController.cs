using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    public class TagController : GenericController<TagDTO, long>
    {

        public TagController(ITagService tagService) : base(tagService)
        {
        }

    }
}
