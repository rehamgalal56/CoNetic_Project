using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace CoNetic.Core.ServicesInterfaces
{
   
    
    public interface IFileService
    {
        public Task<String> UploadImage(IFormFile Image);
        public Task<String> UploadFile(IFormFile File);

    }

   
}
