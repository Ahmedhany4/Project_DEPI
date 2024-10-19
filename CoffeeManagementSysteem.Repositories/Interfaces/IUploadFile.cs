using Microsoft.AspNetCore.Http;

namespace CoffeeManagementSystem.Repositories.Interfaces
{
    public interface IUploadFile
    {
        Task<string> UploadFileAsync(string filePath, IFormFile file);
    }
}
