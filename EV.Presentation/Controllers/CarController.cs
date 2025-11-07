using AutoMapper;
using EV.Application.CustomEntities;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Presentation.RequestModels.UserRequests;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    public class CarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarService _carService;
        private readonly IModelStateCheck _modelStateCheck;

        public CarController(IMapper mapper, ICarService carService, IModelStateCheck modelStateCheck)
        {
            _mapper = mapper;
            _carService = carService;
            _modelStateCheck = modelStateCheck;
        }

        [HttpGet("GetAllCars")]
        public async Task<ActionResult<ResponseDTO<PagedResult<UserCarGetAll>>>> GetAllCars(UserGetAllCarModel userGetAllCarModel)
        {
            var validationResult = _modelStateCheck.CheckModelState<UserGetAllCarModel>(ModelState);

            if (!validationResult.IsSuccess)
            {
                return BadRequest(validationResult);
            }

            var userGetAllCarRequestDTO = _mapper.Map<UserGetAllCarRequestDTO>(userGetAllCarModel);

            var result = await _carService.UserCarGetAll(userGetAllCarRequestDTO);

            if (result.Result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("DeleteCar")]
        public async Task<ActionResult<ResponseDTO<string>>> DeleteCar(int userId, int carId)
        {
            var result = await _carService.UserDeleteCar(userId, carId);


            if (result.Result != null)
            {
                return BadRequest(result);
            }

            if(result.IsSuccess == false && result.Result == null)
            {
                return StatusCode(500, result);
            }

            return Ok(result);
        }

        [HttpPatch("UnDeleteCar")]
        public async Task<ActionResult<ResponseDTO<string>>> UnDeleteCar(int userId, int carId)
        {
            var result = await _carService.UserUnDeleteCar(userId, carId);


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

        [HttpPost("AddCar")]
        public async Task<ActionResult<ResponseDTO<CarAddResponseDTO>>> AddCar([FromForm] CarAddRequestModel carAddRequestModel)
        {
            var validationResult = _modelStateCheck.CheckModelState<CarAddRequestModel>(ModelState);

            if(!validationResult.IsSuccess)
            {
                return BadRequest(validationResult);
            }

            var carAddRequestDTO = _mapper.Map<CarAddRequestDTO>(carAddRequestModel);


            var result = await _carService.AddCar(carAddRequestDTO, carAddRequestModel.Image);

            if (result.Result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("UserCarUpdate")]
        public async Task<ActionResult<ResponseDTO<UserCarUpdateReponse>>> UserCarUpdate([FromForm] UserCarUpdateModel userCarUpdateModel)
        {
            var validationResult = _modelStateCheck.CheckModelState<UserCarUpdateModel>(ModelState);

            if (!validationResult.IsSuccess)
            {
                return BadRequest(validationResult);
            }



            var userCarUpdateDTO = _mapper.Map<UserCarUpdateRequest>(userCarUpdateModel);

            var result = await _carService.UserCarUpdate(userCarUpdateDTO, userCarUpdateModel.Image);

            if (result.Result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpGet("UserViewCarDetail/{UserId}/{VehicleId}")]
        public async Task<ActionResult<ResponseDTO<UserCarDetails>>> UserViewCarDetails(int UserId, int VehicleId)
        {
            UserViewCarDetailsModel userViewCarDetailsModel = new UserViewCarDetailsModel
            {
                UserId = UserId,
                VehicleId = VehicleId
            };

            var userViewCarDetailsRequestDTO = _mapper.Map<UserViewCarDetailsRequestDTO>(userViewCarDetailsModel);

            var result = await _carService.UserCarViewDetailsById(userViewCarDetailsRequestDTO);

            if (result.Result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("UserGetCarDetailsForUpdate/{UserId}/{VehicleId}")]
        public async Task<ActionResult<ResponseDTO<UserCarDetails>>> UserGetCarDetailsForUpdate(int UserId, int VehicleId)
        {
            UserViewCarDetailsModel userViewCarDetailsModel = new UserViewCarDetailsModel
            {
                UserId = UserId,
                VehicleId = VehicleId
            };

            var userViewCarDetailsRequestDTO = _mapper.Map<UserViewCarDetailsRequestDTO>(userViewCarDetailsModel);

            var result = await _carService.UserCarViewDetailsById(userViewCarDetailsRequestDTO);

            if (result.Result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetAuctionVehicleDetails(int vehicleId)
        {
            var vehicle = await _carService.GetCarById(vehicleId);
            if (vehicle == null)
                return NotFound(new { message = "Vehicle not found." });

            return Ok(vehicle);
        }


    }
}
