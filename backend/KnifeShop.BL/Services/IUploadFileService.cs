using Microsoft.AspNetCore.Http;

namespace KnifeShop.BL.Services
{
    public interface IUploadFileService
    {
        /// <summary>
        /// Upload Image
        /// </summary>
        /// <returns>File Patch</returns>
        Task<string?> UploadImage(IFormFile? imageFile);
        /// <summary>
        /// Upload Images
        /// </summary>
        /// <returns>Files Patch</returns>
        Task<List<string>?> UploadImages(List<IFormFile>? imageFiles);
    }
}