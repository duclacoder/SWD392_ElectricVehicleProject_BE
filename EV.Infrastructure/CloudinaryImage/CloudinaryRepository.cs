using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EV.Application.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EV.Infrastructure.CloudinaryImage
{
    public class CloudinaryRepository : ICloudinaryRepository
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryRepository(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageToCloudinaryAsync(IFormFile image)
        {
            await using var stream = image.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.FileName, stream),
                Folder = "UploadImages"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return uploadResult.SecureUrl.ToString();
        }

        public async Task<string> DeleteImage(string publicId)
        {
            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image // optional, default image
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result == "ok")
                return "Deleted successfully";

            return $"Failed to delete. Status: {result.Result}";
        }
    }
}
