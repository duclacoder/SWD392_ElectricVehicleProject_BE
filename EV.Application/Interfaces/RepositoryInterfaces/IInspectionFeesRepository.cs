using EV.Application.CustomEntities;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IInspectionFeesRepository
    {
        Task<IEnumerable<UserInspectionFeesGetAll>> GetAllInspectionFees(int skip, int take);
        Task<int> GetTotalCountInspectionFees();
        Task<UserGetInspectionFeeById?> UserGetInspectionFeeById(int id);
    }
}
