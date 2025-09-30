using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.UserPackages;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class UserPackagesRepository : IUserPackagesRepository
    {
        private readonly Swd392Se1834G2T1Context _context;

        public UserPackagesRepository(Swd392Se1834G2T1Context context) => _context = context;

        public async Task<UserPackagesCustom> CreateUserPackage(UserPackagesDTO userPackages)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.UserName == userPackages.UserName);
            var package = await _context.PostPackages.FirstOrDefaultAsync(b => b.PackageName == userPackages.PackagesName);

            if (user == null || package == null) return null;

            var entity = new UserPackage
            {
                UserId = user.UsersId,
                PackageId = package.PostPackagesId,
                PurchasedPostDuration = userPackages.PurchasedDuration,
                PurchasedAtPrice = userPackages.PurchasedAtPrice,
                PurchasedAt = DateTime.Now,
                Status = "Active"
            };

            _context.UserPackages.Add(entity);
            //await _context.SaveChangesAsync();

            return new UserPackagesCustom
            {
                UserName = userPackages.UserName,
                PackagesName = userPackages.PackagesName,
                PurchasedDuration = userPackages.PurchasedDuration,
                Currency = userPackages.Currency,
                PurchasedAt = entity.PurchasedAt,
                Status = entity.Status
            };
        }

        public async Task<UserPackagesCustom> DeleteUserPackage(int id)
        {

            var userPackages = await _context.UserPackages
                .Include(a => a.User)
                .Include(b => b.Package)
                .FirstOrDefaultAsync(up => up.UserPackagesId == id);
            
            if (userPackages == null) return null;

            userPackages.Status = "InActive";
            //await _context.SaveChangesAsync();
            _context.UserPackages.Update(userPackages);

            return new UserPackagesCustom
            {
                UserName = userPackages.User?.UserName,
                PackagesName = userPackages.Package?.PackageName,
                Currency = userPackages?.Currency,
                PurchasedDuration = userPackages?.PurchasedPostDuration,
                PurchasedAt = userPackages?.PurchasedAt,
                PurchasedAtPrice = userPackages?.PurchasedAtPrice,
                Status = userPackages.Status
            };
        }

        public async Task<IEnumerable<UserPackagesCustom>> GetAllUserPackages(int skip, int take)
        {
            return await _context.UserPackages
                   .Include(a => a.User)
                   .Include(b => b.Package)
                   .OrderByDescending(c => c.PurchasedAt)
                   .Skip(skip)
                   .Take(take)
                   .Select(a => new UserPackagesCustom
                   {
                       UserName = a.User.UserName,
                       PackagesName = a.Package.PackageName,
                       PurchasedDuration = a.PurchasedPostDuration,
                       PurchasedAtPrice = a.PurchasedAtPrice,
                       Currency = a.Currency,
                       PurchasedAt = a.PurchasedAt,
                       Status = a.Status
                   })
                   .ToListAsync();
        }

        public async Task<UserPackagesCustom> GetUserPackageById(int id)
        {
           var x = await _context.UserPackages
                .Include(a => a.User)
                .Include (b => b.Package)
                .FirstOrDefaultAsync(x => x.UserPackagesId == id);

            if (x == null) return null;
            return new UserPackagesCustom
            {
                UserName = x.User?.UserName,
                PackagesName = x.Package?.PackageName,
                PurchasedDuration = x.PurchasedPostDuration,
                PurchasedAtPrice = x.PurchasedAtPrice,
                Currency = x.Currency,
                PurchasedAt = x.PurchasedAt,
                Status = x.Status
            };
        }

        public async Task<IEnumerable<UserPackagesCustom>> GetUserPackageByUserNameAndPackageName(string userName, string packageName, int skip, int take)
        {
            //var items = await _context.UserPackages
            //            .Include(a => a.User)
            //            .Include(b => b.Package)
            //            .Where(a => a.User.UserName == userName && a.Package.PackageName == packageName)
            //            .ToListAsync();

            var userPackages = _context.UserPackages
                                       .Include(a => a.User)
                                       .Include(b => b.Package)
                                       .AsQueryable();
            
            if(!string.IsNullOrEmpty(userName))
            {
                userPackages = userPackages.Where(a => a.User.UserName == userName);
            }

            if(!string.IsNullOrEmpty(packageName))
            {
                userPackages = userPackages.Where(b => b.Package.PackageName == packageName);
            }

            var items = await userPackages.OrderByDescending(a => a.PurchasedAt)
                                          .Skip(skip)
                                          .Take(take)
                                          .ToListAsync();

            return items.Select(a => new UserPackagesCustom
            {
                UserName = a.User?.UserName,
                PackagesName = a.Package?.PackageName,
                PurchasedDuration = a.PurchasedPostDuration,
                PurchasedAtPrice = a.PurchasedAtPrice,
                Currency = a.Currency,
                PurchasedAt = a.PurchasedAt,
                Status = a.Status
            });
        }

        public async Task<UserPackagesCustom> UpdateUserPackage(int id, UserPackagesDTO userPackages)
        {
            var x = await _context.UserPackages.FindAsync(id);
            if (x == null) return null;

            if (!string.IsNullOrEmpty(userPackages.UserName))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userPackages.UserName);
                if (user != null)
                    x.UserId = user.UsersId;
            }

            if (!string.IsNullOrEmpty(userPackages.PackagesName))
            {
                var package = await _context.PostPackages.FirstOrDefaultAsync(p => p.PackageName == userPackages.PackagesName);
                if (package != null)
                    x.PackageId = package.PostPackagesId;
            }
            x.PurchasedPostDuration = userPackages.PurchasedDuration;
            x.PurchasedAtPrice = userPackages.PurchasedAtPrice;
            x.Currency = userPackages.Currency;

            _context.UserPackages.Update(x);

            //await _context.SaveChangesAsync();

            return new UserPackagesCustom
            {
                UserName = x.User?.UserName,
                PackagesName = x.Package?.PackageName,
                PurchasedDuration = x.PurchasedPostDuration,
                PurchasedAtPrice = x.PurchasedAtPrice,
                Currency = x.Currency,
                PurchasedAt = x.PurchasedAt,
                Status = x.Status
            };
        }


    }
}
