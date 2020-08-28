using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    public class UserController : GenericController<AddUserDTO, GetUserDTO, long>
    {

        public UserController(IUserService userService): base(userService)
        {
        }

    }
}
