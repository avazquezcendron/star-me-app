using StarMeApp.Application.Contracts.DTOs.Common;
using System.Threading.Tasks;

namespace StarMeApp.Application.Contracts.Services
{
    public interface IGenericService<TAddDto, TGetDto, TIdTDto>
        where TAddDto : IAddDTO<TIdTDto>
        where TGetDto : IGetDTO<TIdTDto>
    {
        Task<ResponseValueDTO<TGetDto>> GetByIdAsync(TIdTDto id);
        Task<ResponseValueDTO<TAddDto>> GetByIdAsyncForPatch(TIdTDto id);
        Task<ResponseListDTO<TGetDto>> GetAllAsync();
        Task<PagedResponseDTO<TGetDto>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<ResponseValueDTO<TIdTDto>> AddAsync(TAddDto entity);
        Task<ResponseDTO> UpdateAsync(TAddDto entity);
        Task<ResponseDTO> DeleteAsync(TIdTDto id);
    }
}
