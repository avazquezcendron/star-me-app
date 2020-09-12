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
        protected readonly IGenericService<TAddDto, TGetDto, TIdTDto> _service;

        public GenericController(IGenericService<TAddDto, TGetDto, TIdTDto> genericService)
        {
            this._service = genericService;
        }

        //// GET: api/[controller]
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var responseDTO = await this._service.GetAllAsync();
        //    return Ok(responseDTO);
        //}

        // GET: api/[controller]
        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] RequestPaginationDTO paginationDTO) 
        {
            if(paginationDTO.PageNumber.GetValueOrDefault() < 0 || paginationDTO.PageSize.GetValueOrDefault() < 0)
                return BadRequest("Paging parameters cannot be negative.");
            
            var responseDTO = await this._service.GetAllAsync(paginationDTO);
            return Ok(responseDTO);
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TIdTDto id)
        {
            var responseDTO = await this._service.GetByIdAsync(id);
            if (!responseDTO.Succeeded)
            {
                if (responseDTO.Data == null)
                    return NotFound(responseDTO);

                return Ok(responseDTO);
            }

            return Ok(responseDTO);
        }

        // POST api/<controller>
        [HttpPost]
        public virtual async Task<IActionResult> Post(TAddDto dto)
        {
            var responseDTO = await this._service.AddAsync(dto);
            if (!responseDTO.Succeeded)
            {
                if (responseDTO.Data == null)
                    return NotFound(responseDTO);

                return Ok(responseDTO);
            }

            return CreatedAtAction("Get", new { id = responseDTO.Data }, responseDTO);
        }

        // PUT api/[controller]/{id}
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(TIdTDto id, TAddDto dto)
        {
            if (!id.Equals(dto.Id))
            {
                return BadRequest();
            }

            var responseDTO = await this._service.UpdateAsync(dto);
            if (!responseDTO.Succeeded)
                return Ok(responseDTO);

            return Ok(responseDTO);
        }

        //PATCH api/[controller]/{id}
        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(TIdTDto id, JsonPatchDocument<IAddDTO<TIdTDto>> patchDoc)
        {
            var eesponseEntity = await this._service.GetByIdAsyncForPatch(id);
            if (!eesponseEntity.Succeeded)
                return Ok(eesponseEntity);
            else if (eesponseEntity.Data == null)
                return NotFound(eesponseEntity);

            patchDoc.ApplyTo(eesponseEntity.Data, ModelState);

            if (!TryValidateModel(eesponseEntity.Data))
            {
                return ValidationProblem(ModelState);
            }

            var responseDTO = await this._service.UpdateAsync(eesponseEntity.Data);
            if (!responseDTO.Succeeded)
                return Ok(responseDTO);

            return Ok(responseDTO);
        }


        // DELETE api/[controller]/5
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TIdTDto id)
        {
            var responseDTO = await this._service.DeleteAsync(id);
            if (!responseDTO.Succeeded)
                return Ok(responseDTO);

            return Ok(responseDTO);
        }
    }
}
