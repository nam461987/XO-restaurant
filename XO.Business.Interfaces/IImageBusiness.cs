namespace XO.Business.Interfaces
{
    public interface IImageBusiness
    {
        string CropImage(string inStream, string outputFolder, int width, int height);
    }
}
