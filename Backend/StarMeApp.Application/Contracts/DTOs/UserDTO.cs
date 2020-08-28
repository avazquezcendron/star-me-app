using StarMeApp.Application.Contracts.DTOs.Common;

namespace StarMeApp.Application.Contracts.DTOs
{
    public interface IUserDTO : IDTO<long>
    {
    }

    public abstract class UserDTO : IUserDTO
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class GetUserDTO : TagDTO, IGetDTO<long>
    {
    }

    public class AddUserDTO : TagDTO, IAddDTO<long>
    {
    }
}
