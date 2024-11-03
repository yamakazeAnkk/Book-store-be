using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadEbookAsync(IFormFile file);
    }
}