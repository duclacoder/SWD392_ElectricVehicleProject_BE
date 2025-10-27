using AutoMapper;
using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.RequestDTOs.InspectionFeeDTO;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using EV.Presentation.RequestModels.AdminRequests;
using EV.Presentation.RequestModels.UserRequests;

namespace EV.Presentation.Middleware
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

            CreateMap<CarAddRequestModel, CarAddRequestDTO>();

            CreateMap<CarAddRequestDTO, Vehicle>();

            CreateMap<Vehicle, CarAddResponseDTO>();

            CreateMap<UserGetAllCarModel, UserGetAllCarRequestDTO>();

            CreateMap<UserViewCarDetailsModel, UserViewCarDetailsRequestDTO>();

            CreateMap<UserCarUpdateModel, UserCarUpdateRequest>();

            CreateMap<UserCarUpdateRequest, Vehicle>();

            CreateMap<Vehicle, UserCarUpdateReponse>();

            CreateMap<UserGetAllInspectionFeesModel, UserGetAllInspectionFeeRequestDTO>();

            CreateMap<UserGetAllBatteryModel, UserGetAllBatteryRequestDTO>();

            CreateMap<UserViewBatteryDetailsModel, UserViewBatteryDetailsRequestDTO>();

            CreateMap<BatteryAddRequestModel, BatteryAddRequestDTO>();

            CreateMap<BatteryAddRequestDTO, Battery>();

            CreateMap<Battery, BatteryAddResponseDTO>();
        }
    }
}
