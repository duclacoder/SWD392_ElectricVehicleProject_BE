using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.UserPostDTO;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Infrastructure.Repositories
{
    public class UserPostRepository : IUserPostsRepository
    {

        private readonly Swd392Se1834G2T1Context _context;

        public UserPostRepository(Swd392Se1834G2T1Context context) => _context = context;


        public async Task<UserPostCustom> CreateUserPost(CreateUserPostDTO createUserPostDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.UserName == createUserPostDTO.UserName);

            if (user == null)
            {
                return null;
            }

            var vehicle = new Vehicle
            {
                Brand = createUserPostDTO.Vehicle?.Brand,
                Model = createUserPostDTO.Vehicle?.Model,
                Color = createUserPostDTO.Vehicle?.Color,
                Year = createUserPostDTO.Vehicle?.Year,
                Price = createUserPostDTO.Vehicle?.Price,
                Description = createUserPostDTO.Vehicle?.Description,
                BodyType = createUserPostDTO.Vehicle?.BodyType,
                RangeKm = createUserPostDTO.Vehicle?.RangeKm,
                MotorPowerKw = createUserPostDTO.Vehicle?.MotorPowerKw,
                CreatedAt = DateTime.Now,
                Status = "Active"
            };
            _context.Vehicles.Add(vehicle);

            var userPackage = await _context.UserPackages
                                           .FirstOrDefaultAsync(p => p.Package.PackageName == createUserPostDTO.PackageName
                                             && p.Status == "Active");

            if (userPackage == null)
            {
                throw new Exception($"Không tìm thấy gói hợp lệ với tên: {createUserPostDTO.PackageName}");
            }

            var userPost = new UserPost
            {
                UserId = user.UsersId,
                UserPackageId = userPackage.PackageId,
                Vehicle = vehicle,
                PostedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddMonths(1),
                Status = "Active"
            };
            
            _context.UserPosts.Add(userPost);

            if(createUserPostDTO.ImageUrls != null && createUserPostDTO.ImageUrls.Any())
            {
                foreach(var img in  createUserPostDTO.ImageUrls)
                {
                    _context.VehicleImages.Add(new VehicleImage
                    {
                        Vehicle = vehicle,
                        ImageUrl = img,
                    });
                }
            }

            return new UserPostCustom
            {
                UserName = user.UserName,
                Title = createUserPostDTO.Title,
                Vehicle = new VehicleUserPost
                {
                    Brand = createUserPostDTO.Vehicle?.Brand,
                    Model = createUserPostDTO.Vehicle?.Model,
                    Color = createUserPostDTO.Vehicle?.Color,
                    Year = createUserPostDTO.Vehicle?.Year ?? 0,
                    Price = createUserPostDTO.Vehicle?.Price ?? 0,
                    Description = createUserPostDTO.Vehicle?.Description,
                    BodyType = createUserPostDTO.Vehicle?.BodyType,
                    RangeKm = createUserPostDTO.Vehicle?.RangeKm ?? 0,
                    MotorPowerKw = createUserPostDTO.Vehicle?.MotorPowerKw ?? 0,
                    CreatedAt = DateTime.Now,
                    Status = "Active"
                },
                Images = createUserPostDTO.ImageUrls?.ToList(),
                CreatedAt = DateTime.Now,
                Status =  userPost.Status
            };
        }

        public async Task<UserPostCustom> DeleteUserPost(int id)
        {
            var post = await _context.UserPosts
                                     .Include(a => a.Vehicle)
                                     .Include(b => b.Vehicle.VehicleImages)
                                     .Include(a => a.User)
                                     .FirstOrDefaultAsync(a => a.UserPostsId == id);

            if(post == null)
            {
                return null;
            }

            post.Status = "InActive";
            _context.UserPosts.Update(post);

            return new UserPostCustom
            {
                UserName = post.User.UserName,
                Title = "DB trường này ko có!",
                Description = "DB trường này ko có!",
                Vehicle = new VehicleUserPost
                {
                    Brand = post.Vehicle?.Brand,
                    Model = post.Vehicle?.Model,
                    Color = post.Vehicle?.Color,
                    Year = post.Vehicle?.Year ?? 0,
                    Price = post.Vehicle?.Price ?? 0,
                    Description = post.Vehicle?.Description,
                    BodyType = post.Vehicle?.BodyType,
                    RangeKm = post.Vehicle?.RangeKm ?? 0,
                    MotorPowerKw = (int)(post.Vehicle?.MotorPowerKw),
                    CreatedAt = DateTime.Now,
                    Status = post.Status,
                },
                Images = post.Vehicle?.VehicleImages?.Select(a => a.ImageUrl).ToList(),
                CreatedAt = DateTime.Now,
                Status = post.Status
            };
        }

        public async Task<IEnumerable<UserPostCustom>> GetAllUserPosts(int skip, int take, string userName)
        {
            var items = _context.UserPosts
                                 .Include(a => a.User)
                                 .Include(a => a.Vehicle)
                                 .ThenInclude(b => b.VehicleImages)
                                 .AsQueryable();

            if(!string.IsNullOrEmpty(userName))
            {
               items = items.Where(a => a.User.UserName == userName);
            }


            var post = await items.Skip(skip)
                                   .Take(take)
                                   .ToListAsync();

            return post.Select(post => new UserPostCustom
            {
                UserName = userName,
                Title = "DB trường này ko có!",
                Description = "DB trường này ko có!",
                Vehicle = new VehicleUserPost
                {
                    Brand = post.Vehicle?.Brand,
                    Model = post.Vehicle?.Model,
                    Color = post.Vehicle?.Color,
                    Year = post.Vehicle?.Year ?? 0,
                    Price = post.Vehicle?.Price ?? 0,
                    Description = post.Vehicle?.Description,
                    BodyType = post.Vehicle?.BodyType,
                    RangeKm = post.Vehicle?.RangeKm ?? 0,
                    MotorPowerKw = (int)(post.Vehicle?.MotorPowerKw),
                    CreatedAt = post.PostedAt,
                    Status = post.Status
                },
                Images = post.Vehicle?.VehicleImages?.Select(a => a.ImageUrl).ToList(),
                CreatedAt = (DateTime)post.PostedAt,
                Status = post.Status
            });

        }

        public async Task<UserPostCustom> GetUserPostById(int id)
        {
            var post = await _context.UserPosts
                               .Include(a => a.User)
                               .Include(b => b.Vehicle)
                               .ThenInclude(a => a.VehicleImages)
                               .FirstOrDefaultAsync(a => a.UserPostsId == id);
            if(post == null)
            {
                return null;
            }

            return new UserPostCustom
            {
                UserName = post.User.UserName,
                Title = "DB trường này ko có!",
                Description = "DB trường này ko có!",
                Vehicle = new VehicleUserPost
                {
                    Brand = post.Vehicle?.Brand,
                    Model = post.Vehicle?.Model,
                    Color = post.Vehicle?.Color,
                    Year = post.Vehicle?.Year ?? 0,
                    Price = post.Vehicle?.Price ?? 0,
                    Description = post.Vehicle?.Description,
                    BodyType = post.Vehicle?.BodyType,
                    RangeKm = post.Vehicle?.RangeKm ?? 0,
                    MotorPowerKw = (int)(post.Vehicle?.MotorPowerKw),
                    CreatedAt = post.PostedAt,
                    Status = post.Status
                },
                Images = post.Vehicle?.VehicleImages?.Select(a => a.ImageUrl).ToList(),
                CreatedAt = (DateTime)post.PostedAt,
                Status = post.Status
            };
        }

        public Task<UserPostCustom> UpdateUserPost(int id, UpdateUserPostDTO userPost)
        {
            throw new NotImplementedException();
        }
    }
}
