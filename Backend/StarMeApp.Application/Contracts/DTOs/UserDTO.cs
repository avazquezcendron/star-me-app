using StarMeApp.Application.Contracts.DTOs.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StarMeApp.Application.Contracts.DTOs
{
    public interface IUserDTO : IDTO<long>
    {
    }

    public abstract class UserDTO : IUserDTO
    {
        [DataMember]
        [Required]
        public long Id { get; set; }
        [DataMember]
        [Required]
        public string Username { get; set; }
        [DataMember]
        [Required]
        public string Password { get; set; }
        [DataMember]
        [Required]
        public string Email { get; set; }
    }

    public class GetUserDTO : TagDTO, IGetDTO<long>
    {
    }

    public class AddUserDTO : TagDTO, IAddDTO<long>
    {
    }
}
