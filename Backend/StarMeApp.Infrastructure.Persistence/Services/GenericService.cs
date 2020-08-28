using AutoMapper;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Application.Contracts.Services;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            TBe be = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(id));
            
            if(be != null)
                responseDTO.Response = this.MapDTO(be);

            return responseDTO;
        }

        public async Task<ResponseListDTO<TGetDto>> GetAllAsync()
        {
            var responseDTO = new ResponseListDTO<TGetDto>();
            IEnumerable<TBe> listBEs = await this._genericRepository.GetAllAsync();
            IEnumerable<TGetDto> listDTOs = from be in listBEs select (this.MapDTO(be));
            responseDTO.Response = listDTOs;
            responseDTO.TotalListCount = listDTOs.Count();
            return responseDTO;
        }

        public async Task<PagedResponseDTO<TGetDto>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseValueDTO<TIdTDto>> AddAsync(TAddDto dto)
        {
            var responseDTO = new ResponseValueDTO<TIdTDto>();
            TBe entity = this.MapNewEntity(dto).Result;
            IBusinessEntity<TIdTBe> entityResult = await this._genericRepository.AddAsync(entity);
            responseDTO.Response = this._mapper.Map<TIdTDto>(entityResult.Id);
            return responseDTO;            
        }

        public async Task UpdateAsync(TAddDto dto)
        {
            TBe entity = this.MapEntity(dto, await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(dto.Id))).Result;
            await this._genericRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TIdTDto id)
        {
            TBe be = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(id));
            await this._genericRepository.DeleteAsync(be);
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
    }
}
