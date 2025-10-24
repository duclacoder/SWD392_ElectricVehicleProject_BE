using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Application.Services;
using EV.Domain.CustomEntities;
using EV.Presentation.RequestModels.UserRequests;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    public class BatteryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBatteryService _batteryService;
        private readonly IModelStateCheck _modelStateCheck;

        public BatteryController(IMapper mapper, IBatteryService batteryService, IModelStateCheck modelStateCheck)
        {
            _mapper = mapper;
            _batteryService = batteryService;
            _modelStateCheck = modelStateCheck;
        }

        [HttpGet("GetAllBattery")]
        public async Task<ActionResult<ResponseDTO<PagedResult<UserBatteryGetAll>>>> GetAllCars(UserGetAllBatteryModel userGetAllBatteryModel)
        {
            var validationResult = _modelStateCheck.CheckModelState<UserGetAllBatteryModel>(ModelState);

            if (!validationResult.IsSuccess)
            {
                return BadRequest(validationResult);
            }

            var userGetAllBatteryRequestDTO = _mapper.Map<UserGetAllBatteryRequestDTO>(userGetAllBatteryModel);

            var result = await _batteryService.UserBatteryGetAll(userGetAllBatteryRequestDTO);

            if (result.Result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
