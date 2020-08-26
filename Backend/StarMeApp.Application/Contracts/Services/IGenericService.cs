using StarMeApp.Application.Contracts.DTOs.Common;
using System.Threading.Tasks;

namespace StarMeApp.Application.Contracts.Services
{
    public interface IGenericService<TDto, TIdTDto> where TDto : IDTO<TIdTDto>
    {
        Task<ResponseValueDTO<TDto>> GetByIdAsync(TIdTDto id);
        Task<ResponseListDTO<TDto>> GetAllAsync();
        Task<PagedResponseDTO<TDto>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<ResponseValueDTO<TIdTDto>> AddAsync(TDto entity);
        Task UpdateAsync(TDto entity);
        Task DeleteAsync(TIdTDto id);
    }
}
