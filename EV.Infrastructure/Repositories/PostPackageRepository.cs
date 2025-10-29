using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.PostPackageDTO;
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
    public class PostPackageRepository : IPostPackageRepository
    {

        private readonly Swd392Se1834G2T1Context _context;

        public PostPackageRepository(Swd392Se1834G2T1Context context)
        {
            _context = context;
        }

        public async Task<PostPackageCustom> CreatePostPackage(CreatePostPackageRequestDTO createPostPackageRequestDTO)
        {
            var newPostPackage = new PostPackage
            {
                PackageName = createPostPackageRequestDTO.PackageName,
                Description = createPostPackageRequestDTO.Description,
                PostPrice = createPostPackageRequestDTO.PostPrice,
                Currency = createPostPackageRequestDTO.Currency,
                PostDuration = createPostPackageRequestDTO.PostDuration,
                Status = createPostPackageRequestDTO.Status
            };

            await _context.PostPackages.AddAsync(newPostPackage);

            return new PostPackageCustom
            {
                PostPackageId = newPostPackage.PostPackagesId,
                PackageName = newPostPackage.PackageName,
                Description = newPostPackage.Description,
                PostPrice = (decimal)newPostPackage.PostPrice,
                Currency = newPostPackage.Currency,
                PostDuration = newPostPackage.PostDuration,
                Status = newPostPackage.Status
            };
        }

        public async Task<PostPackageCustom> DeletePostPackage(int id)
        {
            var postPackage = await _context.PostPackages.FirstOrDefaultAsync(a => a.PostPackagesId == id);
            if(postPackage == null)
            {
                return null;
            }
            else
            {
                postPackage.Status = "InActive";
                _context.PostPackages.Update(postPackage);
            }
            return new PostPackageCustom
            {
                PostPackageId = postPackage.PostPackagesId,
                PackageName = postPackage.PackageName,
                Description = postPackage.Description,
                PostPrice = postPackage.PostPrice ?? 0,
                Currency = postPackage.Currency,
                PostDuration = postPackage.PostDuration,
                Status = postPackage.Status
            };
        }

        public async Task<IEnumerable<PostPackageCustom>> GetActivePostPackage(int skip, int take)
        {
            var packages = await _context.PostPackages
                                         .Where(a => a.Status == "Active")
                                         .OrderBy(a => a.PostPackagesId)
                                         .Skip(skip)
                                         .Take(take)
                                         .ToListAsync();
            return packages.Select(p => new PostPackageCustom
            {
                PostPackageId = p.PostPackagesId,
                PackageName = p.PackageName,
                Description = p.Description,
                PostDuration = p.PostDuration,
                Currency = p.Currency,
                PostPrice = p.PostPrice,
                Status = p.Status
            });
        }

        public async Task<IEnumerable<PostPackageCustom>> GetAllPostPackage(int skip, int take)
        {
            var packages = await _context.PostPackages
                                         .OrderBy(a => a.PostPackagesId)
                                         .Skip(skip)
                                         .Take(take)
                                         .ToListAsync();

            return packages.Select(p => new PostPackageCustom
            {
                PostPackageId = p.PostPackagesId,
                PackageName = p.PackageName,
                Description = p.Description,
                PostDuration = p.PostDuration,
                Currency = p.Currency,
                PostPrice = p.PostPrice ?? 0,
                Status = p.Status
            });
        }

        public async Task<PostPackageCustom> GetPostPackageById(int id)
        {
            var existing = await _context.PostPackages
                                         .FirstOrDefaultAsync(a => a.PostPackagesId == id);
            if(existing == null)
            {
                return null;
            }

            return new PostPackageCustom
            {
                PostPackageId = existing.PostPackagesId,
                PackageName = existing.PackageName,
                Description = existing.Description,
                PostDuration = existing.PostDuration,
                Currency = existing.Currency,
                PostPrice = existing.PostPrice,
                Status = existing.Status
            };
        }

        public async Task<IEnumerable<PostPackageCustom>> SearchPostPackageByPackageName(string packageName, int skip, int take)
        {
            if (string.IsNullOrEmpty(packageName))
            {
                return await GetAllPostPackage(skip, take);
            }

            var packages = await _context.PostPackages
                                         .Where(a => a.PackageName.ToLower().Contains(packageName.ToLower()))
                                         .OrderBy(a => a.PostPackagesId)
                                         .Skip(skip)
                                         .Take(take)
                                         .ToListAsync();

            return packages.Select(p => new PostPackageCustom
            {
                PostPackageId = p.PostPackagesId,
                PackageName = p.PackageName,
                Description = p.Description,
                PostDuration = p.PostDuration,
                Currency = p.Currency,
                PostPrice = p.PostPrice,
                Status = p.Status
            });
        }

        public async Task<PostPackageCustom> UpdatePostPackage(int id, CreatePostPackageRequestDTO updatePostPackageRequestDTO)
        {
            var existing = await _context.PostPackages.FirstOrDefaultAsync(p => p.PostPackagesId == id);
            if (existing == null)
            {
                return null;
            }
            
            existing.PackageName = updatePostPackageRequestDTO.PackageName;
            existing.Description = updatePostPackageRequestDTO.Description;
            existing.PostPrice = updatePostPackageRequestDTO.PostPrice;
            existing.Currency = updatePostPackageRequestDTO.Currency;
            existing.PostDuration = updatePostPackageRequestDTO.PostDuration;
            existing.Status = updatePostPackageRequestDTO.Status;

            _context.PostPackages.Update(existing);
            return new PostPackageCustom
            {
                PostPackageId = existing.PostPackagesId,
                PackageName = existing.PackageName,
                Description = existing.Description,
                PostDuration = existing.PostDuration,
                Currency = existing.Currency,
                PostPrice = existing.PostPrice,
                Status = existing.Status
            };
        }


    }
}
