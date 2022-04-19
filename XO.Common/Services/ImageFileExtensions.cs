using System.IO;

namespace XO.Common.Services
{
    public static class ImageFileExtensions
    {
        public static string ChangeImageFileNameBySize(string imageName, int width, int height)
        {
            return imageName + "-" + width + "x" + height;
        }
        public static string CheckThumbnailExist(string imagelUrl, int width, int height)
        {
            string fileExtension = Path.GetExtension(imagelUrl);
            string fileName = Path.GetFileNameWithoutExtension(imagelUrl);
            string fileNameOutput = ChangeImageFileNameBySize(fileName, width, height);
            string output = imagelUrl.Substring(0, imagelUrl.Length - (fileName.Length + fileExtension.Length))
                + "thumb/" + fileNameOutput + fileExtension;

            return output;
        }
    }
}
