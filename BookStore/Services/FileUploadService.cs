using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly FirebaseStorageService _firebaseStorageService;

        public FileUploadService(FirebaseStorageService firebaseStorageService)
        {
            _firebaseStorageService = firebaseStorageService;
        }
        public async Task<string> UploadEbookAsync(IFormFile file)
        {
            
            if (file == null)
            {
                throw new ArgumentException("Invalid eBook file");
            }
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                return await _firebaseStorageService.UploadImageAsync(stream, fileName, file.ContentType);
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || !file.ContentType.StartsWith("image/"))
            { 
                throw new ArgumentException("Invalid image file");
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                return await _firebaseStorageService.UploadImageAsync(stream, fileName, file.ContentType);
            }
        }
    }
}