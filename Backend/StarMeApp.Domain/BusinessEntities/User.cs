using StarMeApp.Domain.Common;

namespace StarMeApp.Domain.BusinessEntities
{
    public class User : AuditableBusinessEntity
    {
        public User()
        {
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
