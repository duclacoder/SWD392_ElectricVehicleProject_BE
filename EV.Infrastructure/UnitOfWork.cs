using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Infrastructure.DBContext;
using EV.Infrastructure.Repositories;

namespace EV.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Swd392Se1834G2T1Context _context;       

        // CONSTRUCTOR INJECTION for DbContext
        public UnitOfWork(Swd392Se1834G2T1Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //Repository interfaces
        private IUserRepository _userRepository;
        public IUserRepository userRepository => _userRepository ??= new UserRepository(_context);

        private IAuthRepository _authRepository;
        public IAuthRepository authRepository => _authRepository ??= new AuthRepository(_context);

        private ICarRepository _carRepository;
        public ICarRepository carRepository => _carRepository ??= new CarRepository(_context);

        private IUserPackagesRepository _userPackagesRepository;
        public IUserPackagesRepository userPackagesRepository =>_userPackagesRepository ??= new UserPackagesRepository(_context);

        private IUserPostsRepository _userPostsRepository;
        public IUserPostsRepository userPostsRepository => _userPostsRepository ??= new UserPostRepository(_context);

        public IInspectionFeesRepository _inspectionFeesRepository;
        public IInspectionFeesRepository inspectionFeesRepository => _inspectionFeesRepository ??= new InspectionFeesRepository(_context);

        private IAuctionRepository _auctionRepository;
        public IAuctionRepository auctionRepository => _auctionRepository ??= new AuctionRepository(_context);

        private IAuctionsFeeRepository _auctionsFeeRepository;
        public IAuctionsFeeRepository auctionsFeeRepository => _auctionsFeeRepository ??= new AuctionsFeeRepository(_context);

        private IPostPackageRepository _postPackageRepository;
        public IPostPackageRepository postPackageRepository => _postPackageRepository ??= new PostPackageRepository(_context);

        private IPaymentRepository _paymentRepository;
        public IPaymentRepository paymentRepository => _paymentRepository ??= new PaymentRepository(_context);


        private IBatteryRepository _batteryRepository;
        public IBatteryRepository batteryRepository => _batteryRepository ??= new BatteryRepository(_context);

        private IAuctionBidRepository _auctionBidRepository;
        public IAuctionBidRepository auctionBidRepository => _auctionBidRepository ??= new AuctionBidRepository(_context);


        private IAuctionParticipantRepository _auctionParticipantRepository;
        public IAuctionParticipantRepository auctionParticipantRepository => _auctionParticipantRepository ??= new AuctionParticipantRepository(_context);

        public IGenericRepository<T> GetGenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // IDisposable Implementation
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
