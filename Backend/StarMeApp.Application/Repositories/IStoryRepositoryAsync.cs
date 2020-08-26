using StarMeApp.Domain.BusinessEntities;
using System.Threading.Tasks;

namespace StarMeApp.Application.Repositories
{
    public interface IStoryRepositoryAsync : IGenericRepositoryAsync<Story, long>
    {
        Task<bool> IsUniqueTitleAsync(string barcode);
    }
}
