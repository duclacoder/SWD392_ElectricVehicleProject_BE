using AutoMapper;
using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using EV.Presentation.RequestModels.AdminRequests;
using EV.Presentation.RequestModels.UserRequests;

namespace EV.Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginRequestModel, LoginRequestDTO>();

            CreateMap<GetAllUsersModel, GetAllUsersRequestDTO>();

            CreateMap<CreateUserModel, CreateUserRequestDTO>();

            CreateMap<UpdateUserModel, UpdateUserRequestDTO>();

            CreateMap<CreateUserRequestDTO, User>();

            CreateMap<ProfileUpdateRequestModel, ProfileUpdateRequestDTO>();

            CreateMap<User, UserProfileUpdate>();
        }
    }
}
