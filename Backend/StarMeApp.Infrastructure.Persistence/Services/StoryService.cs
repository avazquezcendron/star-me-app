using AutoMapper;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarMeApp.Infrastructure.Persistence.Services
{
    public class StoryService : GenericService<AddStoryDTO, GetStoryDTO, Story, long, long>, IStoryService
    {
        private readonly ITagRepositoryAsync _tagRepositoryAsync;
        public StoryService(IMapper mapper, IStoryRepositoryAsync storyRepository, ITagRepositoryAsync tagRepositoryAsync) : base(mapper, storyRepository)
        {
            this._tagRepositoryAsync = tagRepositoryAsync;
        }

        protected override async Task<Story> MapNewEntity(AddStoryDTO dto)
        {
            Story be = await base.MapNewEntity(dto);
            await MapTags(dto, be);
            return be;
        }

        protected override GetStoryDTO MapDTO(Story be)
        {
            var dto = base.MapDTO(be);
            var tags = new List<GetTagDTO>();
            foreach (var tag in be.Tags)
            {
                tags.Add(new GetTagDTO() { Id = tag.TagId, Name = tag.Tag.Name });
            }
            dto.Tags = tags;
            return dto;
        }

        protected override AddStoryDTO MapDTOForPatch(Story be)
        {
            var dto = base.MapDTOForPatch(be);
            var tags = new List<AddTagDTO>();
            foreach (var tag in be.Tags)
            {
                tags.Add(new AddTagDTO() { Id = tag.TagId, Name = tag.Tag.Name });
            }
            dto.Tags = tags;
            return dto;
        }

        protected override async Task<Story> MapEntity(AddStoryDTO dto, Story be)
        {
            var entity = await base.MapEntity(dto, be);            
            await this.MapTags(dto, be);
            return entity;
        }

        private async Task MapTags(AddStoryDTO dto, Story entity)
        {
            entity.ClearTags();
            foreach (var tagDto in dto.Tags)
            {
                var tag = await this._tagRepositoryAsync.GetByIdAsync(this._mapper.Map<long>(tagDto.Id));// TODO: refactor this: move to a more general method that allows get any related entity.
                if (tag == null)
                {
                    tag = this._mapper.Map<Tag>(tagDto);
                    tag.Id = 0;
                }
                entity.AddTag(tag);
            }
        }
    }
}
