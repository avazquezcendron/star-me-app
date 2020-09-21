using StarMeApp.Domain.BusinessEntities;
using System.Threading.Tasks;

namespace StarMeApp.Application.Repositories
{
    public interface ITagRepositoryAsync : IGenericRepositoryAsync<Tag, long>
    {
        Task<Tag> GetTagWithStoriesAsync(long tagId);
    }
}
