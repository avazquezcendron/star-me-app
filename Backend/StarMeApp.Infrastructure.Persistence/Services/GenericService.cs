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
    public abstract class GenericService<TDto, TBe, TIdTDto, TIdTBe> : IGenericService<TDto, TIdTDto> 
        where TDto : IDTO<TIdTDto>
        where TBe : IBusinessEntity<TIdTBe>
    {
        protected IGenericRepositoryAsync<TBe, TIdTBe> _genericRepository { get; set; }
        protected IMapper _mapper;

        public GenericService(IMapper mapper, IGenericRepositoryAsync<TBe, TIdTBe> genericRepository)
        {
            this._mapper = mapper;
            this._genericRepository = genericRepository;
        }

        public async Task<ResponseValueDTO<TDto>> GetByIdAsync(TIdTDto id)
        {
            var responseDTO = new ResponseValueDTO<TDto>();
            TBe be = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(id));
            
            if(be != null)
                responseDTO.Response = this._mapper.Map<TDto>(be);

            return responseDTO;
        }

        public async Task<ResponseListDTO<TDto>> GetAllAsync()
        {
            var responseDTO = new ResponseListDTO<TDto>();
            IEnumerable<TBe> listBEs = await this._genericRepository.GetAllAsync();
            IEnumerable<TDto> listDTOs = from be in listBEs select (this._mapper.Map<TDto>(be));
            responseDTO.Response = listDTOs;
            responseDTO.TotalListCount = listDTOs.Count();
            return responseDTO;
        }

        public async Task<PagedResponseDTO<TDto>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseValueDTO<TIdTDto>> AddAsync(TDto dto)
        {
            var responseDTO = new ResponseValueDTO<TIdTDto>();
            TBe entity = this._mapper.Map<TBe>(dto);
            IBusinessEntity<TIdTBe> entityResult = await this._genericRepository.AddAsync(entity);
            responseDTO.Response = this._mapper.Map<TIdTDto>(entityResult.Id);
            return responseDTO;
            
        }

        public async Task UpdateAsync(TDto dto)
        {
            TBe entity = this._mapper.Map<TBe>(dto);
            await this._genericRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TIdTDto id)
        {
            TBe be = await this._genericRepository.GetByIdAsync(this._mapper.Map<TIdTBe>(id));
            await this._genericRepository.DeleteAsync(be);
        }
    }
}
