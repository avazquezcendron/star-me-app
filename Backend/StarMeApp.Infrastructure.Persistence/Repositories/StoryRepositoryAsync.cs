
using Microsoft.EntityFrameworkCore;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;
using StarMeApp.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;

namespace StarMeApp.Infrastructure.Persistence.Repositories
{
    public class StoryRepositoryAsync : GenericRepositoryAsync<Story, long>, IStoryRepositoryAsync
    {
        private readonly DbSet<Story> _stories;

        public StoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _stories = dbContext.Set<Story>();
        }

        public Task<bool> IsUniqueTitleAsync(string title)
        {
            return _stories
                .AllAsync(p => p.Title != title);
        }
    }
}
