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

        public async Task<string> DeleteImage(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return "Image URL or publicId is required.";

            // If user passes full URL, extract public ID
            var publicId = imageUrl;
            if (imageUrl.StartsWith("http"))
            {
                var parts = imageUrl.Split("/upload/");
                if (parts.Length == 2)
                {
                    // Remove version and extension
                    publicId = parts[1]
                        .Substring(parts[1].IndexOf('/') + 1); // remove folder
                    publicId = Path.ChangeExtension(publicId, null); // remove .jpg
                }
            }

            var result = await _cloudinary.DestroyAsync(new DeletionParams(publicId));

            if (result.Result == "ok")
                return $"Deleted: {publicId}";

            return $"Failed to delete. Cloudinary says: {result.Result}";
        }
    }
}
