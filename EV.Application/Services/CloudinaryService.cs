using EV.Application.Interfaces.RepositoryInterfaces;
using EV.Application.Interfaces.ServiceInterfaces;
using EV.Application.ResponseDTOs;
using Microsoft.AspNetCore.Http;

namespace EV.Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly ICloudinaryRepository _cloudinaryRepository;

        public CloudinaryService(ICloudinaryRepository cloudinaryRepository)
        {
            _cloudinaryRepository = cloudinaryRepository;
        }

        public async Task<ResponseDTO<string>> UploadImageAsync(IFormFile image)
        {
            var uploadResult = await _cloudinaryRepository.UploadImageToCloudinaryAsync(image);

            return new ResponseDTO<string>("Image uploaded successfully", true, uploadResult);
        }

        public async Task<ResponseDTO<string>> DeleteImageAsync(string publicId)
        {
            var deleteResult = await _cloudinaryRepository.DeleteImage(publicId);

            return new ResponseDTO<string>("Image deletion result", true, deleteResult);
        }
    }
}
