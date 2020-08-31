using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Application.Contracts.Services;
using System.Threading.Tasks;

namespace StarMeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericController<TAddDto, TGetDto, TIdTDto> : ControllerBase
        where TAddDto : IAddDTO<TIdTDto>
        where TGetDto : IGetDTO<TIdTDto>
    {
        private readonly IGenericService<TAddDto, TGetDto, TIdTDto> _genericService;

        public GenericController(IGenericService<TAddDto, TGetDto, TIdTDto> genericService)
        {
            this._genericService = genericService;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responseDTO = await this._genericService.GetAllAsync();
            return Ok(responseDTO);
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(TIdTDto id)
        {
            var responseDTO = await this._genericService.GetByIdAsync(id);
            if (!responseDTO.Succeeded)
            {
                if (responseDTO.Response == null)
                    return NotFound(responseDTO);

                return Ok(responseDTO);
            }

            return Ok(responseDTO);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(TAddDto dto)
        {
            var responseDTO = await this._genericService.AddAsync(dto);
            if (!responseDTO.Succeeded)
            {
                if (responseDTO.Response == null)
                    return NotFound(responseDTO);

                return Ok(responseDTO);
            }

            return CreatedAtAction("Get", new { id = responseDTO.Response }, responseDTO);
        }

        // PUT api/[controller]/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(TIdTDto id, TAddDto dto)
        {
            if (!id.Equals(dto.Id))
            {
                return BadRequest();
            }

            var responseDTO = await this._genericService.UpdateAsync(dto);
            if (!responseDTO.Succeeded)
                return Ok(responseDTO);

            return NoContent();
        }

        //PATCH api/[controller]/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(TIdTDto id, JsonPatchDocument<IAddDTO<TIdTDto>> patchDoc)
        {
            var eesponseEntity = await this._genericService.GetByIdAsyncForPatch(id);
            if (!eesponseEntity.Succeeded)
                return Ok(eesponseEntity);
            else if (eesponseEntity.Response == null)
                return NotFound(eesponseEntity);

            patchDoc.ApplyTo(eesponseEntity.Response, ModelState);

            if (!TryValidateModel(eesponseEntity.Response))
            {
                return ValidationProblem(ModelState);
            }

            var responseDTO = await this._genericService.UpdateAsync(eesponseEntity.Response);
            if (!responseDTO.Succeeded)
                return Ok(responseDTO);

            return NoContent();
        }


        // DELETE api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TIdTDto id)
        {
            var responseDTO = await this._genericService.DeleteAsync(id);
            if (!responseDTO.Succeeded)
                return Ok(responseDTO);

            return NoContent();
        }
    }
}
