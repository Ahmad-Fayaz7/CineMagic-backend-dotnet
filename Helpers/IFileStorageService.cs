namespace CineMagic.Helpers
{
    public interface IFileStorageService
    {
        Task DeleteFile(string fileRoute, string folderName);

        Task<string> SaveFile(string folderName, IFormFile file);
        Task<string> EditFile(string folderName, IFormFile file, string fileRoute);
    }


}
