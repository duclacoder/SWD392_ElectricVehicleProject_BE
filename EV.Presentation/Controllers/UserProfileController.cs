using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserRequestDTO;
using EV.Application.ResponseDTOs;
using EV.Domain.CustomEntities;
using EV.Presentation.RequestModels.UserRequests;
using Microsoft.AspNetCore.Mvc;

namespace EV.Presentation.Controllers
{
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserProfileController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }


        [HttpPost("UpdateUserProfile")]
        public async Task<ActionResult<ResponseDTO<UserProfileUpdate>>> UpdateUserProfile([FromBody] ProfileUpdateRequestModel profileUpdateRequest)
        {
            var updateProfileRequestDTO = _mapper.Map<ProfileUpdateRequestDTO>(profileUpdateRequest);

            var result = await _userService.UserUpdateProfile(updateProfileRequestDTO);

            if(result.Result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<ResponseDTO<GetUserProfileById>>> GetUserById(int id) 
        { 

            var result = await _userService.GetUserProfileById(id);

            if (result.Result != null)
            {
                return Ok(result);
            }

            return NotFound(result);
        }
    }
}
