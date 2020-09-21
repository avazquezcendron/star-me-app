using AutoMapper;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.DTOs.Common;
using StarMeApp.Domain.BusinessEntities;
using StarMeApp.Domain.Common;

namespace StarMeApp.Application.Mappings
{
    public class Profiles: Profile
    {
        public Profiles()
        {
            CreateMap<AddStoryDTO, Story>().ForMember(dest => dest.Tags, opt => opt.Ignore());
            CreateMap<Story, GetStoryDTO>().ForMember(dest => dest.Tags, opt => opt.Ignore());
            CreateMap<Story, AddStoryDTO>().ForMember(dest => dest.Tags, opt => opt.Ignore());
            CreateMap<AddTagDTO, Tag>().ReverseMap();//Reverse is used for PATCH
            CreateMap<Tag, GetTagDTO>();
            CreateMap<Tag, GetTagWithStoriesDTO>().ForMember(dest => dest.Stories, opt => opt.Ignore()); ;
            CreateMap<AddUserDTO, User>().ReverseMap();//Reverse is used for PATCH
            CreateMap<User, GetUserDTO>();

            CreateMap<AuditInfoStruct, AuditInfoStructDTO>().ReverseMap();
        }
    }

}
