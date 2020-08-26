using System;
using System.Collections.Generic;
using System.Text;
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
            CreateMap<Story, StoryDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<AuditInfoStruct, AuditInfoStructDTO>().ReverseMap();
        }
    }
}
