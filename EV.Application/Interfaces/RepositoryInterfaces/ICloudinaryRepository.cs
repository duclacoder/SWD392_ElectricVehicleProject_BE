using Microsoft.AspNetCore.Http;

namespace EV.Application.Interfaces.RepositoryInterfaces
{
    public interface ICloudinaryRepository
    {
        Task<string> UploadImageToCloudinaryAsync(IFormFile image);
        Task<string> DeleteImage(string imageUrl);
    }
}