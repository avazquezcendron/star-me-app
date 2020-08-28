using StarMeApp.Application.Contracts.DTOs;

namespace StarMeApp.Application.Contracts.Services
{
    public interface IUserService: IGenericService<AddUserDTO, GetUserDTO, long>
    {

    }
}
