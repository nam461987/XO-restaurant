using XO.Business.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace XO.Business
{
    public class UploadBusiness : IUploadBusiness
    {
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UploadBusiness(IMapper mapper,
            IHostingEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> MultipleUpload(IFormFile file, string forwardFolder, string uploadFolder)
        {
            string root = Path.GetDirectoryName(Path.GetDirectoryName(_hostingEnvironment.ContentRootPath));
            string imageStr = string.Empty;

            Random r = new Random();
            string filename = r.Next().ToString() + "_" + file.FileName;

            //create folder by month
            string now = DateTime.Now.ToString("MMyyyy");
            forwardFolder = @"" + root + forwardFolder + "";
            string newFolder = @"" + root + uploadFolder + "" + now + "\\";

            string firstPath = forwardFolder + filename;
            string destinationPath = newFolder + filename;

            using (var fileStream = new FileStream(firstPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(newFolder))
                {
                    //save file to exists folder
                    System.IO.File.Move(firstPath, destinationPath);
                    imageStr = "/uploads/" + now + "/" + filename;
                    return imageStr;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(newFolder);
                DirectoryInfo dii = Directory.CreateDirectory(newFolder + "\\thumb");
                //save file to new folder
                System.IO.File.Move(firstPath, destinationPath);
                imageStr = "/uploads/" + now + "/" + filename;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return imageStr;
        }
    }
}
