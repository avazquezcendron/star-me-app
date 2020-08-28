using AutoMapper;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;

namespace StarMeApp.Infrastructure.Persistence.Services
{
    public class TagService : GenericService<AddTagDTO, GetTagDTO, Tag, long, long>, ITagService
    {

        public TagService(IMapper mapper, ITagRepositoryAsync tagRepository): base (mapper, tagRepository)
        {
        }

    }
}
