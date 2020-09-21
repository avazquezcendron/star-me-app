using Microsoft.AspNetCore.Mvc;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;
using System.Threading.Tasks;

namespace StarMeApp.Controllers
{
    public class TagsController : GenericController<AddTagDTO, GetTagDTO, long>
    {

        public TagsController(ITagService tagService) : base(tagService)
        {
        }

        // GET: api/tags/{id}/stories
        [HttpGet]
        [Route("{id}/stories")]
        public async Task<IActionResult> GetStoriesByTag(long? id)
        {
            if (id.GetValueOrDefault() > 0)
            {
                var responseDTO = await (this._service as ITagService).GetTagWithStoriesAsync(id.GetValueOrDefault());
                return Ok(responseDTO);
            }

            return BadRequest();
        }
    }
}
