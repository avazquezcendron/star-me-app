using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.Common;
using StarMeApp.Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace StarMeApp.Infrastructure.Persistence.Repositories
{
    public abstract class GenericRepositoryAsync<T, TId> : IGenericRepositoryAsync<T, TId> where T : class, IBusinessEntity<TId>
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.Id = default;
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(IRequestPaginationDTO requestPaginationDTO)
        {
            if (requestPaginationDTO.PageSize.GetValueOrDefault() > 0 && requestPaginationDTO.PageNumber.GetValueOrDefault() > 0)
            {
                requestPaginationDTO.TotalSize = await _dbContext
                                                    .Set<T>()
                                                    .CountAsync();
                return await _dbContext
                .Set<T>()
                .OrderByDescending(x => x.Id)
                .Skip((requestPaginationDTO.PageNumber.GetValueOrDefault() - 1) * requestPaginationDTO.PageSize.GetValueOrDefault())
                .Take(requestPaginationDTO.PageSize.GetValueOrDefault())
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                return await _dbContext
                    .Set<T>()
                    .ToListAsync();
            }
        }
    }
}
