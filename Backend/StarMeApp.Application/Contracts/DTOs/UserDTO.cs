using StarMeApp.Application.Contracts.DTOs.Common;

namespace StarMeApp.Application.Contracts.DTOs
{
    public class UserDTO : IDTO<long>
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
