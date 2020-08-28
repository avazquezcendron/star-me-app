
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;
using StarMeApp.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public override async Task<IEnumerable<Story>> GetAllAsync()
        {
            return await _stories.Include(s => s.Tags).ThenInclude(t => t.Tag).ToListAsync();
        }

        public override async Task<Story> GetByIdAsync(long id)
        {
            return await _stories.Include(s => s.Tags).ThenInclude(t => t.Tag).FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task UpdateAsync(Story entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> IsUniqueTitleAsync(string title)
        {
            return _stories
                .AllAsync(p => p.Title != title);
        }
    }
}
