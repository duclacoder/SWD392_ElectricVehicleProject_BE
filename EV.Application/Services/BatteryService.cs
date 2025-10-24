using AutoMapper;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;

namespace EV.Application.Services
{
    public class BatteryService : IBatteryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BatteryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<PagedResult<UserBatteryGetAll>>> UserBatteryGetAll(UserGetAllBatteryRequestDTO userGetAllBatteryRequestDTO)
        {
            var batteries = await _unitOfWork.batteryRepository.GetAllBatteryByUserId(userGetAllBatteryRequestDTO.UserId, (userGetAllBatteryRequestDTO.Page - 1) * userGetAllBatteryRequestDTO.PageSize
                , userGetAllBatteryRequestDTO.PageSize);
            var totalItems = await _unitOfWork.batteryRepository.GetTotalCountBatteryByUserId(userGetAllBatteryRequestDTO.UserId);


            var pagedResult = new PagedResult<UserBatteryGetAll>
            {
                Items = batteries.ToList(),
                TotalItems = totalItems,
                Page = userGetAllBatteryRequestDTO.Page,
                PageSize = userGetAllBatteryRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / userGetAllBatteryRequestDTO.PageSize)
            };
            if (batteries.Count() == 0)
            {
                return new ResponseDTO<PagedResult<UserBatteryGetAll>>("Can not find any battery", false, null);
            }


            return new ResponseDTO<PagedResult<UserBatteryGetAll>>("Get all batteries successfully", true, pagedResult);
        }
    }
}
