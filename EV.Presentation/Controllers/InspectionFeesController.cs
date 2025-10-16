using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.InspectionFeeDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Presentation.RequestModels.UserRequests;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    public class InspectionFeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModelStateCheck _modelStateCheck;
        private readonly IInspectionFeesService _inspectionFeesService;

        public InspectionFeesController(IMapper mapper, IModelStateCheck modelStateCheck, IInspectionFeesService inspectionFeesService)
        {
            _mapper = mapper;
            _modelStateCheck = modelStateCheck;
            _inspectionFeesService = inspectionFeesService;
        }


        [HttpGet("GetAllInspectionFees")]
        public async Task<ActionResult<ResponseDTO<PagedResult<UserInspectionFeesGetAll>>>> GetAllInspectionFees(UserGetAllInspectionFeesModel userGetAllInspectionFeesModel)
        {
            var validationResult = _modelStateCheck.CheckModelState<UserGetAllInspectionFeesModel>(ModelState);
            if (!validationResult.IsSuccess)
            {
                return BadRequest(validationResult);
            }

            var userGetAllInspectionFeeRequestDTO = _mapper.Map<UserGetAllInspectionFeeRequestDTO>(userGetAllInspectionFeesModel);

            var result = await _inspectionFeesService.UserGetAllInspectionFees(userGetAllInspectionFeeRequestDTO);

            if (result.Result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("GetInspectionFeeById/{id}")]
        public async Task<ActionResult<ResponseDTO<UserGetInspectionFeeById>>> GetInspectionFeeById(int id)
        {
            var result = await _inspectionFeesService.UserGetInspectionFeeById(id);
            if (result.Result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost("RequestInspection")]
        public async Task<ActionResult<ResponseDTO<string>>> RequestInspection()
        {
            return Ok("Fuck off");
        }
    }
}
