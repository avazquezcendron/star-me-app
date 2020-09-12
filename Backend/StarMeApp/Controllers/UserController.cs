using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    public class UsersController : GenericController<AddUserDTO, GetUserDTO, long>
    {

        public UsersController(IUserService userService): base(userService)
        {
        }

    }
}
