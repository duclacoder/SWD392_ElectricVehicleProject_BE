using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Presentation.RequestModels.AdminRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO<PagedResult<AdminGetAllUsers>>>> GetAllUsers([FromQuery] GetAllUsersModel getAllUsersModel)
        {
            var getAllUserRequestDTO = _mapper.Map<GetAllUsersRequestDTO>(getAllUsersModel);

            var response = await _userService.GetAllUsers(getAllUserRequestDTO);



            return Ok(response);
        }
    }
}