using AutoMapper;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Application.Contracts.Services;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarMeApp.Infrastructure.Persistence.Services
{
    public class TagService : GenericService<AddTagDTO, GetTagDTO, Tag, long, long>, ITagService
    {

        public TagService(IMapper mapper, ITagRepositoryAsync tagRepository): base (mapper, tagRepository)
        {
        }

        public virtual async Task<ResponseValueDTO<GetTagWithStoriesDTO>> GetTagWithStoriesAsync(long tagId)
        {
            var responseDTO = new ResponseValueDTO<GetTagWithStoriesDTO>();
            try
            {
                Tag be = await (this._repository as ITagRepositoryAsync).GetTagWithStoriesAsync(this._mapper.Map<long>(tagId));
                if (be != null)
                    responseDTO.Data = this.MapWithStoriesDTO(be);
                else
                    responseDTO.AddMessage(MessageKind.Error, "Entity with ID '" + tagId + "' not found.");
            }
            catch (Exception e)
            {
                responseDTO.AddMessage(MessageKind.Error, e.Message);
            }


            return responseDTO;
        }

        protected GetTagWithStoriesDTO MapWithStoriesDTO(Tag be)
        {
            var dto = this._mapper.Map<GetTagWithStoriesDTO>(be);
            var stories = new List<GetStoryDTO>();
            foreach (var story in be.Stories)
            {
                stories.Add(new GetStoryDTO() { 
                    Id = story.StoryId, 
                    Title = story.Story.Title,
                    AuditInfo = this._mapper.Map<AuditInfoStructDTO>(story.Story.AuditInfo),
                    Summary = story.Story.Summary });
            }
            dto.Stories = stories;
            return dto;
        }
    }
}
