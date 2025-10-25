using EV.Application.ResponseDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.Interfaces.ServiceInterfaces
{
    public interface ICloudinaryService
    {
        Task<ResponseDTO<string>> UploadImageAsync(IFormFile image);

        Task<ResponseDTO<string>> DeleteImageAsync(string publicId);
    }
}
