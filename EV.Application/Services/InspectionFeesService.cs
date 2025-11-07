using AutoMapper;
using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.RequestDTOs.InspectionFeeDTO;
using EV.Application.ResponseDTOs;

namespace EV.Application.Services
{
    public class InspectionFeesService : IInspectionFeesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InspectionFeesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<PagedResult<UserInspectionFeesGetAll>>> UserGetAllInspectionFees(UserGetAllInspectionFeeRequestDTO userGetAllInspectionFeeRequestDTO)
        {
            var inspectionFees = await _unitOfWork.inspectionFeesRepository.GetAllInspectionFees((userGetAllInspectionFeeRequestDTO.Page - 1) * userGetAllInspectionFeeRequestDTO.PageSize
                , userGetAllInspectionFeeRequestDTO.PageSize);

            var totalCount = await _unitOfWork.inspectionFeesRepository.GetTotalCountInspectionFees();

            var pagedResult = new PagedResult<UserInspectionFeesGetAll>
            {
                Items = inspectionFees.ToList(),
                TotalItems = totalCount,
                Page = userGetAllInspectionFeeRequestDTO.Page,
                PageSize = userGetAllInspectionFeeRequestDTO.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / userGetAllInspectionFeeRequestDTO.PageSize)
            };

            if (inspectionFees.Count() == 0)
            {
                return new ResponseDTO<PagedResult<UserInspectionFeesGetAll>>("No inspection fees found.", false, null);
            }

            return new ResponseDTO<PagedResult<UserInspectionFeesGetAll>>("Get all inspection fees successfully", true, pagedResult);
        }

        public async Task<ResponseDTO<UserGetInspectionFeeById>> UserGetInspectionFeeById(int id)
        {
            var inspectionFee = await _unitOfWork.inspectionFeesRepository.UserGetInspectionFeeById(id);

            if (inspectionFee == null)
            {
                return new ResponseDTO<UserGetInspectionFeeById>("Inspection fee not found.", false, null);
            }

            return new ResponseDTO<UserGetInspectionFeeById>("Get inspection fee by id successfully", true, inspectionFee);
        }
    }
}
