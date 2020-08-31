using AutoMapper;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Application.Contracts.Services;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarMeApp.Infrastructure.Persistence.Services
{
    public abstract class GenericService<TAddDto, TGetDto, TBe, TIdTDto, TIdTBe> : IGenericService<TAddDto, TGetDto, TIdTDto>
        where TAddDto : IAddDTO<TIdTDto>
        where TGetDto : IGetDTO<TIdTDto>
        where TBe : IBusinessEntity<TIdTBe>
    {
        protected IGenericRepositoryAsync<TBe, TIdTBe> _genericRepository { get; set; }
        protected IMapper _mapper;

        public GenericService(IMapper mapper, IGenericRepositoryAsync<TBe, TIdTBe> genericRepository)
        {
            this._mapper = mapper;
            this._genericRepository = genericRepository;
        }

        public async Task<ResponseValueDTO<TGetDto>> GetByIdAsync(TIdTDto id)
        {
            var responseDTO = new ResponseValueDTO<TGetDto>();
            try
            {
                TBe be = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(id));
                if (be != null)
                    responseDTO.Response = this.MapDTO(be);
                else
                    responseDTO.AddMessage(MessageKind.Error, "Entity with ID '" + id + "' not found.");
            }
            catch (Exception e)
            {
                responseDTO.AddMessage(MessageKind.Error, e.Message);
            }


            return responseDTO;
        }

        public async Task<ResponseValueDTO<TAddDto>> GetByIdAsyncForPatch(TIdTDto id)
        {
            var responseDTO = new ResponseValueDTO<TAddDto>();
            try
            {
                TBe be = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(id));
                if (be != null)
                    responseDTO.Response = this.MapDTOForPatch(be);
                else
                    responseDTO.AddMessage(MessageKind.Error, "Entity with ID '" + id + "' not found.");
            }
            catch (Exception e)
            {
                responseDTO.AddMessage(MessageKind.Error, e.Message);
            }
            return responseDTO;
        }

        public async Task<ResponseListDTO<TGetDto>> GetAllAsync()
        {
            var responseDTO = new ResponseListDTO<TGetDto>();
            try
            {
                IEnumerable<TBe> listBEs = await this._genericRepository.GetAllAsync();
                IEnumerable<TGetDto> listDTOs = from be in listBEs select (this.MapDTO(be));
                responseDTO.Response = listDTOs;
                responseDTO.TotalListCount = listDTOs.Count();
            }
            catch (Exception e)
            {
                //TODO: improve exception handling with more detailed exceptions.
                responseDTO.AddMessage(MessageKind.Error, e.Message);
            }

            return responseDTO;
        }

        public async Task<PagedResponseDTO<TGetDto>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseValueDTO<TIdTDto>> AddAsync(TAddDto dto)
        {
            var responseDTO = new ResponseValueDTO<TIdTDto>();
            TBe entity = await this.MapNewEntity(dto);
            try
            {
                IBusinessEntity<TIdTBe> entityResult = await this._genericRepository.AddAsync(entity);
                responseDTO.Response = this._mapper.Map<TIdTDto>(entityResult.Id);
            }
            catch (Exception e)
            {
                responseDTO.AddMessage(MessageKind.Error, e.Message);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(TAddDto dto)
        {
            var responseDTO = new ResponseDTO();
            try
            {
                var entitySaved = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(dto.Id));
                if (entitySaved != null)
                {
                    TBe entity = await this.MapEntity(dto, entitySaved);
                    await this._genericRepository.UpdateAsync(entity);
                    responseDTO.AddMessage(MessageKind.Info, "The entity "+ typeof(TAddDto) +" with ID '"+ dto.Id +"' was successfully updated.");
                }
                else
                {
                    responseDTO.AddMessage(MessageKind.Error, "Entity with ID '" + dto.Id + "' not found.");
                }
            }
            catch (Exception e)
            {
                responseDTO.AddMessage(MessageKind.Error, e.Message);
            }

            return responseDTO;
        }

        public async Task<ResponseDTO> DeleteAsync(TIdTDto id)
        {
            var responseDTO = new ResponseDTO();
            try
            {
                var entitySaved = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(id));
                if (entitySaved != null)
                {
                    await this._genericRepository.DeleteAsync(entitySaved);
                    responseDTO.AddMessage(MessageKind.Info, "The entity " + typeof(TAddDto) + " with ID '" + id + "' was successfully deleted.");
                }
                else
                {
                    responseDTO.AddMessage(MessageKind.Error, "Entity with ID '" + id + "' not found.");
                }
            }
            catch (Exception e)
            {
                responseDTO.AddMessage(MessageKind.Error, e.Message);
            }

            return responseDTO;
        }

        protected virtual async Task<TBe> MapNewEntity(TAddDto dto)
        {
            return await Task.Run(() => this._mapper.Map<TBe>(dto));
        }

        protected virtual async Task<TBe> MapEntity(TAddDto dto, TBe be)
        {
            return await Task.Run(() => this._mapper.Map(dto, be));
        }

        protected virtual TGetDto MapDTO(TBe be)
        {
            return this._mapper.Map<TGetDto>(be);
        }
        protected virtual TAddDto MapDTOForPatch(TBe be)
        {
            return this._mapper.Map<TAddDto>(be);
        }

    }
}
