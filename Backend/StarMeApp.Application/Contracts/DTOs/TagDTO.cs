using StarMeApp.Application.Contracts.DTOs.Common;

namespace StarMeApp.Application.Contracts.DTOs
{
    public class TagDTO : IDTO<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
