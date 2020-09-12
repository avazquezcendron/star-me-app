
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StarMeApp.Application.Contracts.DTOs.Common;
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

        public override async Task<IEnumerable<Story>> GetAllAsync(IRequestPaginationDTO requestPaginationDTO)
        {
            if (requestPaginationDTO.PageSize.GetValueOrDefault() > 0 && requestPaginationDTO.PageNumber.GetValueOrDefault() > 0)
            {
                requestPaginationDTO.TotalSize = await _stories.CountAsync();
                return await _stories
                        .Include(s => s.Tags)
                        .ThenInclude(t => t.Tag)
                        .OrderByDescending(x => x.Id)
                        .Skip((requestPaginationDTO.PageNumber.GetValueOrDefault() - 1) * requestPaginationDTO.PageSize.GetValueOrDefault())
                        .Take(requestPaginationDTO.PageSize.GetValueOrDefault())
                        .AsNoTracking()
                        .ToListAsync();
            }
            else
            {
                return await _stories
                       .Include(s => s.Tags)
                       .ThenInclude(t => t.Tag)
                       .OrderByDescending(x => x.AuditInfo.CreatedAt)
                       .ToListAsync();
            }
        }

        public override async Task<Story> GetByIdAsync(long id)
        {
            return await _stories.Include(s => s.Tags).ThenInclude(t => t.Tag).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Story>> GetStoriesByTag(long tagId)
        {
            return await _stories
                .Include(s => s.Tags)
                .ThenInclude(t => t.Tag)
                .Where(s => s.Tags.Any(t => t.TagId == tagId))
                .OrderByDescending(x => x.AuditInfo.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Story>> GetStoriesByTitle(string title)
        {
            return await _stories
                .Where(s => EF.Functions.Like(s.Title, "%"+title+"%"))
                .Include(s => s.Tags)
                .ThenInclude(t => t.Tag)
                .OrderByDescending(x => x.AuditInfo.CreatedAt)
                .ToListAsync();
        }
    }
}
