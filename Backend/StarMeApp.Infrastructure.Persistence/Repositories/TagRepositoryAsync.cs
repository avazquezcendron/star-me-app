using Microsoft.EntityFrameworkCore;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;
using StarMeApp.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarMeApp.Infrastructure.Persistence.Repositories
{
    public class TagRepositoryAsync : GenericRepositoryAsync<Tag, long>, ITagRepositoryAsync
    {
        private readonly DbSet<Tag> _tags;
        public TagRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _tags = dbContext.Set<Tag>();
        }
        public async Task<Tag> GetTagWithStoriesAsync(long tagId)
        {
            var tag = await this._tags
                   .Include(t => t.Stories)
                   .ThenInclude(s => s.Story)
                   .FirstOrDefaultAsync(t => t.Id == tagId);
            
            tag.Stories = tag.Stories.OrderByDescending(s => s.Story.AuditInfo.CreatedAt).ToList();

            return tag;
        }
    }
}
