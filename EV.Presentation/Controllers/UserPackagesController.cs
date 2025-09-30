using AutoMapper;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.UserPackages;
using EV.Application.RequestDTOs.UserPackagesDTO;
using EV.Application.ResponseDTOs;
using EV.Application.Services;
using EV.Domain.CustomEntities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EV.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPackagesController : ControllerBase
    {
        private readonly IUserPackagesServices  _userPackagesServices;
        private readonly IMapper _mapper;

        public UserPackagesController(IUserPackagesServices userPackagesServices, IMapper mapper)
        {
            _userPackagesServices = userPackagesServices;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseDTO<PagedResult<UserPackagesCustom>>>> GetAll([FromQuery] GetAllUserPackageRequestDTO request)
        {
            var response = await _userPackagesServices.GetAllUserPackages(request);
            return Ok(response);
        }

        // Lấy UserPackage theo Id
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ResponseDTO<UserPackagesCustom>>> GetById(int id)
        {
            var response = await _userPackagesServices.GetUserPackageById(id);
            return Ok(response);
        }

        // Tạo mới UserPackage
        [HttpPost("Create")]
        public async Task<ActionResult<ResponseDTO<UserPackagesCustom>>> Create([FromBody] UserPackagesDTO dto)
        {
            var response = await _userPackagesServices.CreateUserPackage(dto);
            return Ok(response);
        }

        // Cập nhật UserPackage
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ResponseDTO<UserPackagesCustom>>> Update(int id, [FromBody] UserPackagesDTO dto)
        {
            var response = await _userPackagesServices.UpdateUserPackage(id, dto);
            return Ok(response);
        }

        // Xóa (soft delete) UserPackage
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ResponseDTO<UserPackagesCustom>>> Delete(int id)
        {
            var response = await _userPackagesServices.DeleteUserPackage(id);
            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetUserPackagesByFilter([FromQuery] GetAllUserPackageRequestDTO request)
        {
            var result = await _userPackagesServices.GetUserPackageByUserNameAndPackageName(request);
            return Ok(result);
        }
    }
}
