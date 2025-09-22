using AutoMapper;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Presentation.RequestModels.UserRequests;

namespace EV.Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginRequestModel, LoginRequestDTO>();
        }
    }
}
