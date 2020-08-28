using AutoMapper;
using StarMeApp.Application.Contracts.DTOs;
using StarMeApp.Application.Contracts.Services;
using StarMeApp.Application.Repositories;
using StarMeApp.Domain.BusinessEntities;

namespace StarMeApp.Infrastructure.Persistence.Services
{
    public class UserService : GenericService<AddUserDTO, GetUserDTO, User, long, long>, IUserService
    {

        public UserService(IMapper mapper, IUserRepositoryAsync userRepository): base (mapper, userRepository)
        {
        }

    }
}
