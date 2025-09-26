using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.AdminRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
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

        [HttpGet("GetUserById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO<User>>> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            return Ok(response);
        }

        [HttpPost("CreateUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO<User>>> CreateUser([FromBody] CreateUserModel createUserModel)
        {
            var createUserRequestDTO = _mapper.Map<CreateUserRequestDTO>(createUserModel);
            var response = await _userService.CreateUser(createUserRequestDTO);
            return Ok(response);
        }

        [HttpPut("UpdateUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO<User>>> UpdateUser(int id, [FromBody] UpdateUserModel updateUserModel)
        {
            var updateUserRequestDTO = _mapper.Map<UpdateUserRequestDTO>(updateUserModel);
            var response = await _userService.UpdateUser(id, updateUserRequestDTO);
            return Ok(response);
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDTO<bool>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            return Ok(response);
        }
    }
}