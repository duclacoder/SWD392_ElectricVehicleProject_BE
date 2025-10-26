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
        public async Task<ActionResult<ResponseDTO<PagedResult<UserBatteryGetAll>>>> GetAllBattery(UserGetAllBatteryModel userGetAllBatteryModel)
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

        [HttpGet("UserViewBatteryDetail/{UserId}/{BatteryId}")]
        public async Task<ActionResult<ResponseDTO<UserCarDetails>>> UserViewBatteryDetails(int UserId, int BatteryId)
        {
            UserViewBatteryDetailsModel userViewBatteryDetailsModel = new UserViewBatteryDetailsModel
            {
                UserId = UserId,
                BatteryId = BatteryId
            };

            var userViewBatteryDetailsDTO = _mapper.Map<UserViewBatteryDetailsRequestDTO>(userViewBatteryDetailsModel);

            var result = await _batteryService.UserBatteryViewDetailsById(userViewBatteryDetailsDTO);

            if (result.Result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("DeleteBattery")]
        public async Task<ActionResult<ResponseDTO<string>>> DeleteBattery(int userId, int batteryId)
        {
            var result = await _batteryService.UserDeleteBattery(userId, batteryId);


            if (result.Result != null)
            {
                return BadRequest(result);
            }

            if (result.IsSuccess == false && result.Result == null)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpPatch("UnDeleteBattery")]
        public async Task<ActionResult<ResponseDTO<string>>> UnDeleteBattery(int userId, int batteryId)
        {
            var result = await _batteryService.UserUnDeleteBattery(userId, batteryId);


            if (result.Result != null)
            {
                return BadRequest(result);
            }

            if (result.IsSuccess == false && result.Result == null)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpPost("AddBattery")]
        public async Task<ActionResult<ResponseDTO<BatteryAddResponseDTO>>> AddBattery([FromForm] BatteryAddRequestModel batteryAddRequestModel)
        {
            var validationResult = _modelStateCheck.CheckModelState<BatteryAddRequestModel>(ModelState);

            if (!validationResult.IsSuccess)
            {
                return BadRequest(validationResult);
            }

            var batteryAddRequestDTO = _mapper.Map<BatteryAddRequestDTO>(batteryAddRequestModel);

            var result = await _batteryService.AddBattery(batteryAddRequestDTO, batteryAddRequestModel.ImageUpload);

            if (result.Result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
