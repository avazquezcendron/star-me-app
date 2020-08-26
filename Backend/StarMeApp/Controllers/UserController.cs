using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;

namespace StarMeApp.Controllers
{
    public class UserController : GenericController<UserDTO, long>
    {

        public UserController(IUserService userService): base(userService)
        {
        }

    }
}
