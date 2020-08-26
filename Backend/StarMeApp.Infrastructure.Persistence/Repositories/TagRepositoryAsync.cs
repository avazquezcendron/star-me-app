using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;
using StarMeApp.Infrastructure.Persistence.Contexts;

namespace StarMeApp.Infrastructure.Persistence.Repositories
{
    public class TagRepositoryAsync : GenericRepositoryAsync<Tag, long>, ITagRepositoryAsync
    {
        public TagRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
