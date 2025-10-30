using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.UserPostDTO;
using EV.Domain.CustomEntities;
using EV.Domain.Entities;
using EV.Infrastructure.CloudinaryImage;
using EV.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EV.Infrastructure.Repositories
{
    public class UserPostRepository : IUserPostsRepository
    {

        private readonly Swd392Se1834G2T1Context _context;
        private readonly ICloudinaryRepository _cloudinaryRepository;

        public UserPostRepository(Swd392Se1834G2T1Context context, ICloudinaryRepository cloudinaryRepository)
        {
            _context = context;
            _cloudinaryRepository = cloudinaryRepository;
        }


        public async Task<UserPostCustom> CreateUserPost(CreateUserPostDTO createUserPostDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.UsersId == createUserPostDTO.UserId);

            if (user == null)
            {
                return null;
            }

            var availableUserPackage = await _context.UserPackages
                                              .FirstOrDefaultAsync(p => p.UserId == createUserPostDTO.UserId
                                                                      && p.Status == "Pending");

            if (availableUserPackage == null)
            {
                throw new Exception("Bạn không có gói đăng bài nào hợp lệ hoặc đã sử dụng hết. Vui lòng mua gói mới để đăng bài.");
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

            var userPost = new UserPost
            {
                UserId = user.UsersId,
                UserPackageId = availableUserPackage.UserPackagesId,
                Vehicle = vehicle,
                PostedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddMonths(1),
                Status = "Active"
            };
            
            _context.UserPosts.Add(userPost);

            availableUserPackage.Status = "Used";
            _context.UserPackages.Update(availableUserPackage);


            var uploadedImageUrls = new List<string>();
            if (createUserPostDTO.ImageUrls != null && createUserPostDTO.ImageUrls.Any())
            {
                foreach(var img in  createUserPostDTO.ImageUrls)
                {
                    var imageUrls = await _cloudinaryRepository.UploadImageToCloudinaryAsync(img);
                    uploadedImageUrls.Add(imageUrls);
                    _context.VehicleImages.Add(new VehicleImage
                    {
                        Vehicle = vehicle,
                        ImageUrl = imageUrls
                    });
                }
            }

            await _context.SaveChangesAsync();

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
                Images = uploadedImageUrls,
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
                UserPostId = post.UserPostsId,
                UserName = post.User.UserName,
                Title = post.Vehicle?.Brand + " " + post.Vehicle?.Model + " " + post.Vehicle?.Color + " " + post.Vehicle?.Year,
                //Description = "DB trường này ko có!",
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

        public async Task<(IEnumerable<UserPostCustom> Items, int TotalCount)> GetAllUserPosts(int skip, int take, int? userId)
        {
            var items = _context.UserPosts
                                 .Include(a => a.User)
                                 .Include(a => a.Vehicle)
                                 .ThenInclude(b => b.VehicleImages)
                                 .Where(a => a.Status == "Active")
                                 .AsQueryable();

            if(userId.HasValue)
            {
               items = items.Where(a => a.User.UsersId == userId);
            }
            else
            {
                items = items.Where(a => a.Status == "Active");
            }


            var totalCount = await items.CountAsync();

            var post = await items
                                    .OrderByDescending(a => a.PostedAt)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToListAsync();

            var result = post.Select(post => new UserPostCustom
            {
                UserPostId = post.UserPostsId,
                UserName = post.User?.UserName,
                Title = post.Vehicle?.Brand + " " + post.Vehicle?.Model + " " + post.Vehicle?.Color + " " + post.Vehicle?.Year,
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

            return (result, totalCount);

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
                UserPostId = post.UserPostsId,
                UserName = post.User.UserName,
                Title = post.Vehicle?.Brand + " " + post.Vehicle?.Model + " " + post.Vehicle?.Color + " " + post.Vehicle?.Year,
                //Description = "DB trường này ko có!",
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

            public async Task UpdateUserPost(int id, UpdateUserPostDTO userPost)
            {
                var existing = await _context.UserPosts
                                             .Include(a => a.Vehicle)
                                             .ThenInclude(a => a.User)
                                             .FirstOrDefaultAsync(a => a.UserPostsId == id);
                if (existing == null)
                {
                throw new Exception("Bài đăng không tồn tại.");
                }
                if (existing.Status == "Active")
                {
                    throw new Exception("Không thể chỉnh sửa bài đăng đã được duyệt. Vui lòng xóa và đăng bài mới.");
                }
            }
    }
}
