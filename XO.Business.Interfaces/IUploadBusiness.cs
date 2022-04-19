using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XO.Business.Interfaces
{
    public interface IUploadBusiness
    {
        Task<string> MultipleUpload(IFormFile file, string forwardFolder, string uploadFolder);
    }
}
