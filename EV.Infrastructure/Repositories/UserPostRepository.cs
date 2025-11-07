using EV.Application.CustomEntities;
using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.RequestDTOs.UserPostDTO;
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

            if (createUserPostDTO.Vehicle == null && createUserPostDTO.Battery == null)
            {
                throw new Exception("Vui lòng cung cấp thông tin xe hoặc pin.");
            }

            //if (createUserPostDTO.Vehicle != null && createUserPostDTO.Battery != null)
            //{
            //    throw new Exception("Chỉ được đăng xe HOẶC pin trong 1 bài, không được cả hai.");
            //}

            int? vehicleId = null;
            int? batteryId = null;
            var uploadedImageUrls = new List<string>();

            if (createUserPostDTO.Vehicle != null)
            {

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

                if (createUserPostDTO.ImageUrls != null && createUserPostDTO.ImageUrls.Any())
                {
                    foreach (var img in createUserPostDTO.ImageUrls)
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
                vehicleId = vehicle.VehiclesId;
            }
            else if(createUserPostDTO.Battery != null)
            {
                var battery = new Battery
                {
                    UserId = createUserPostDTO.UserId,
                    BatteryName = createUserPostDTO.Battery.BatteryName,
                    Brand = createUserPostDTO.Battery.Brand,
                    Description = createUserPostDTO.Battery.Description,
                    Capacity = createUserPostDTO.Battery.Capacity,
                    Voltage = createUserPostDTO.Battery.Voltage,
                    WarrantyMonths = createUserPostDTO.Battery.WarrantyMonths,
                    Price = createUserPostDTO.Battery.Price,
                    Currency = createUserPostDTO.Battery.Currency,
                    CreatedAt = DateTime.Now,
                    Status = "Active"
                };

                _context.Batteries.Add(battery);

                if (createUserPostDTO.ImageUrls != null && createUserPostDTO.ImageUrls.Any())
                {
                    foreach (var img in createUserPostDTO.ImageUrls)
                    {
                        var imageUrls = await _cloudinaryRepository.UploadImageToCloudinaryAsync(img);
                        uploadedImageUrls.Add(imageUrls);
                        _context.BatteryImages.Add(new BatteryImage
                        {
                            Battery = battery,
                            ImageUrl = imageUrls
                        });
                    }
                }
                await _context.SaveChangesAsync();
                batteryId = battery.BatteriesId;
            }

            var userPost = new UserPost
            {
                UserId = user.UsersId,
                UserPackageId = availableUserPackage.UserPackagesId,
                VehicleId = vehicleId,
                BatteryId = batteryId,
                PostedAt = DateTime.Now,
                ExpiredAt = DateTime.Now.AddMonths(1),
                Status = "Active"
            };
            _context.UserPosts.Add(userPost);

            availableUserPackage.Status = "Used";
            _context.UserPackages.Update(availableUserPackage);
            await _context.SaveChangesAsync();


            return new UserPostCustom
            {
                UserName = user.UserName,
                Title = createUserPostDTO.Title,
                Vehicle = createUserPostDTO.Vehicle != null ? new VehicleUserPost
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
                } : null,
                Battery = createUserPostDTO.Battery != null ? new BatteryUserPost
                {
                    BatteryName = createUserPostDTO.Battery.BatteryName,
                    Brand = createUserPostDTO.Battery.Brand,
                    Description = createUserPostDTO.Battery.Description,
                    Capacity = createUserPostDTO.Battery.Capacity ?? 0,
                    Voltage = (double)(createUserPostDTO.Battery.Voltage ?? 0),
                    WarrantyMonths = createUserPostDTO.Battery.WarrantyMonths ?? 0,
                    Price = createUserPostDTO.Battery.Price,
                    Currency = createUserPostDTO.Battery.Currency,
                    CreatedAt = DateTime.Now,
                    Status = "Active"
                } : null,
                Images = uploadedImageUrls,
                CreatedAt = DateTime.Now,
                Status =  userPost.Status
            };
        }

        public async Task<UserPostCustom> DeleteUserPost(int id)
        {
            var post = await _context.UserPosts
                                     .Include(a => a.Vehicle)
                                        .ThenInclude(b => b.VehicleImages)
                                     .Include(a => a.User)
                                     .Include(a => a.Battery)
                                        .ThenInclude(b => b.BatteryImages)
                                     .FirstOrDefaultAsync(a => a.UserPostsId == id);

            if(post == null)
            {
                return null;
            }

            post.Status = "InActive";
            _context.UserPosts.Update(post);
            await _context.SaveChangesAsync();

            var isVehiclePost = post.Vehicle != null; 
            var images = isVehiclePost ? post.Vehicle?.VehicleImages?.Select(x => x.ImageUrl).ToList() : post.Battery?.BatteryImages?.Select(x => x.ImageUrl).ToList();

            return new UserPostCustom
            {
                UserPostId = post.UserPostsId,
                UserName = post.User.UserName,
                Title = isVehiclePost ? $"{post.Vehicle?.Brand} {post.Vehicle?.Model} {post.Vehicle?.Color} {post.Vehicle?.Year}" : $"{post.Battery.Brand} {post.Battery.BatteryName} ({post.Battery.Capacity}Ah)",
                Vehicle = isVehiclePost ? new VehicleUserPost
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
                } : null,
                Battery = !isVehiclePost ? new BatteryUserPost
               {
                    BatteryName = post.Battery.BatteryName,
                    Brand = post.Battery.Brand,
                    Description = post.Battery.Description,
                    Capacity = post.Battery.Capacity ?? 0,
                    Voltage = (double)(post.Battery.Voltage ?? 0),
                    WarrantyMonths = post.Battery.WarrantyMonths ?? 0,
                    Price = (decimal)post.Battery.Price,
                    Currency = post.Battery.Currency,
                    CreatedAt = (DateTime)post.Battery.CreatedAt,
                    Status = post.Status,
               }: null,
                Images = images ?? new List<string?>(),
                CreatedAt = DateTime.Now,
                Status = post.Status
            };
        }

        public async Task<(IEnumerable<UserPostCustom> Items, int TotalCount)> GetAllUserPosts(int skip, int take, int? userId, bool? isVehiclePost = null)
        {
            var items = _context.UserPosts
                                 .Include(a => a.User)
                                 .Include(a => a.Vehicle)
                                 .ThenInclude(b => b.VehicleImages)
                                 .Include(a => a.Battery)
                                 .ThenInclude(b => b.BatteryImages)
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

            if (isVehiclePost.HasValue)
            {
                if (isVehiclePost.Value == true)
                {
                    items = items.Where(a => a.VehicleId != null);
                }
                else
                {
                    items = items.Where(a => a.BatteryId != null);
                }
            }


            var totalCount = await items.CountAsync();

            var post = await items
                                    .OrderByDescending(a => a.PostedAt)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToListAsync();

            var result = post.Select(post =>
            {
                bool vehiclePost = post.VehicleId.HasValue;

                return new UserPostCustom
                {
                    UserPostId = post.UserPostsId,
                    UserName = post.User?.UserName,
                    Title = vehiclePost ? $"{post.Vehicle?.Brand} {post.Vehicle?.Model} {post.Vehicle?.Color} {post.Vehicle?.Year}" : $"{post.Battery?.Brand} {post.Battery?.BatteryName} ({post.Battery?.Capacity}Ah)",
                    Vehicle = vehiclePost ? new VehicleUserPost
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
                        Status = post.Vehicle.Status
                    } : null,
                    Battery = !vehiclePost ? new BatteryUserPost
                    {
                        BatteryName = post.Battery.BatteryName,
                        Brand = post.Battery.Brand,
                        Description = post.Battery.Description,
                        Capacity = post.Battery.Capacity ?? 0,
                        Voltage = (double)(post.Battery.Voltage ?? 0),
                        WarrantyMonths = post.Battery.WarrantyMonths ?? 0,
                        Price = (decimal)post.Battery.Price,
                        Currency = post.Battery.Currency,
                        CreatedAt = (DateTime)post.Battery.CreatedAt,
                        Status = post.Battery.Status,
                    } : null,
                    Images = vehiclePost ? post.Vehicle?.VehicleImages?.Select(a => a.ImageUrl).ToList() : post.Battery?.BatteryImages?.Select(a => a.ImageUrl).ToList(),
                    CreatedAt = (DateTime)post.PostedAt,
                    Status = post.Status
                };
            });

            return (result, totalCount);

        }

        public async Task<UserPostCustom> GetUserPostById(int id)
        {
            var post = await _context.UserPosts
                               .Include(a => a.User)
                               .Include(b => b.Vehicle)
                               .ThenInclude(a => a.VehicleImages)
                               .Include(a =>a.Battery)
                               .ThenInclude(b => b.BatteryImages)
                               .FirstOrDefaultAsync(a => a.UserPostsId == id);
            if(post == null)
            {
                return null;
            }

            string title = "";
            if (post.Vehicle != null)
            {
                title = $"{post.Vehicle.Brand} {post.Vehicle.Model} {post.Vehicle.Color} {post.Vehicle.Year}";
            }
            else if (post.Battery != null)
            {
                title = $"{post.Battery?.Brand} {post.Battery?.BatteryName} ({post.Battery?.Capacity}Ah)";
            }

            return new UserPostCustom
            {
                UserPostId = post.UserPostsId,
                UserName = post.User.UserName,
                Title = title,
                Vehicle = post.Vehicle != null ?  new VehicleUserPost
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
                } : null,
                Battery = post.Battery != null ? new BatteryUserPost
                {
                    BatteryName = post.Battery.BatteryName,
                    Brand = post.Battery.Brand,
                    Description = post.Battery.Description,
                    Capacity = post.Battery.Capacity ?? 0,
                    Voltage = (double)(post.Battery.Voltage ?? 0),
                    WarrantyMonths = post.Battery.WarrantyMonths ?? 0,
                    Price = (decimal)post.Battery.Price,
                    Currency = post.Battery.Currency,
                    CreatedAt = (DateTime)post.PostedAt,
                    Status = post.Status
                } : null,
                Images = post.Vehicle != null ? post.Vehicle?.VehicleImages?.Select(a => a.ImageUrl).ToList() : post.Battery?.BatteryImages?.Select(a => a.ImageUrl).ToList(),
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
