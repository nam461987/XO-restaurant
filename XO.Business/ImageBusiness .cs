using ImageProcessor.Imaging;
using XO.Business.Interfaces;
using XO.Common.Services;
using Microsoft.AspNetCore.Hosting;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.Processing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace XO.Business
{
    public class ImageBusiness : IImageBusiness
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ImageBusiness(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public string CropImage(string inStream, string outputFolder, int width, int height)
        {
            string root = _hostingEnvironment.WebRootPath;
            string fileExtension = Path.GetExtension(inStream);
            string fileName = Path.GetFileNameWithoutExtension(inStream);
            string fileNameOutput = ImageFileExtensions
                .ChangeImageFileNameBySize(fileName, width, height);
            string output = inStream.Substring(0, inStream.Length - (fileName.Length + fileExtension.Length))
                + outputFolder + "/" + fileNameOutput + fileExtension;

            AnchorPosition Anchor = AnchorPosition.Center;

            Image imgPhoto = Image.FromFile(root + inStream);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (Anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int)(height - (sourceHeight * nPercent));
                        break;
                    default:
                        destY = (int)((height - (sourceHeight * nPercent)) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (Anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int)(width - (sourceWidth * nPercent));
                        break;
                    default:
                        destX = (int)((width - (sourceWidth * nPercent)) / 2);
                        break;
                }
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            bmPhoto.Save(root + output);
            bmPhoto.Dispose();

            return output;
        }
    }
}
