using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericController<TDto, TIdTDto> : ControllerBase where TDto : IDTO<TIdTDto>
    {
        private readonly IGenericService<TDto, TIdTDto> _genericService;

        public GenericController(IGenericService<TDto, TIdTDto> genericService)
        {
            this._genericService = genericService;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this._genericService.GetAllAsync());
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(TIdTDto id)
        {
            var responseDTO = await this._genericService.GetByIdAsync(id);
            if(responseDTO.Response == null)
                return NotFound();

            return Ok(responseDTO);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(TDto dto)
        {
            var responseDTO = await this._genericService.AddAsync(dto);
            return CreatedAtAction("Get", new { id = responseDTO.Response }, responseDTO);
        }

        // PUT api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(TIdTDto id, TDto dto)
        {
            if (!id.Equals(dto.Id))
            {
                return BadRequest();
            }

            try
            {
                await this._genericService.UpdateAsync(dto);
            }
            catch (Exception e)
            {
                throw e;
            }

            return NoContent();
        }

        // DELETE api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TIdTDto id)
        {
            await this._genericService.DeleteAsync(id);
            return NoContent();
        }
    }
}
