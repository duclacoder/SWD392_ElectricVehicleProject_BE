using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Repository interfaces
        IGenericRepository<T> GetGenericRepository<T>() where T : class;

        IUserRepository userRepository { get; }

        IAuthRepository authRepository { get; }

        IUserPackagesRepository userPackagesRepository { get; }

        IUserPostsRepository userPostsRepository { get; }
        //Single commit point
        Task<int> SaveChangesAsync();
    }
}
