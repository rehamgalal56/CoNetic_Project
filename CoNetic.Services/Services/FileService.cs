using CoNetic.Core.ServicesInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Services.Services
{
    public class FileService(IWebHostEnvironment webHostEnvironment):IFileService
    {
       
        private readonly string ImagesPath = $"{webHostEnvironment.WebRootPath}/Images";
        private readonly string FilesPath = $"{webHostEnvironment.WebRootPath}/Files";

        public async Task<String> UploadImage(IFormFile Image)
        {
            var path=Path.Combine(ImagesPath, Image.FileName);
            using var stream = File.Create(path);
            await Image.CopyToAsync(stream);
            return path;
        }
        public async Task<String> UploadFile(IFormFile file)
        {
            var path = Path.Combine(FilesPath, file.FileName);
            using var stream = File.Create(path);
            await file.CopyToAsync(stream);
            return path;

        }


    }
}
