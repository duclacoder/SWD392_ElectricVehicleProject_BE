using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : class;

        IUserRepository userRepository { get; }

        ICarRepository carRepository { get; }

        IAuthRepository authRepository { get; }

        IUserPackagesRepository userPackagesRepository { get; }

        IAuctionRepository auctionRepository { get; }

        IUserPostsRepository userPostsRepository { get; }

        IPostPackageRepository postPackageRepository { get; }

        IPaymentRepository paymentRepository { get; }

        IInspectionFeesRepository inspectionFeesRepository { get; }

        IBatteryRepository batteryRepository { get; }

        IAuctionBidRepository auctionBidRepository { get; }

        IAuctionsFeeRepository auctionsFeeRepository { get; }

        IAuctionParticipantRepository auctionParticipantRepository { get; }
        IDashboardRepository dashboardRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
