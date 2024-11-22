using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.DTOs
{
    public class UploadFilesDto
    {
        [FromForm(Name = "ImageFile")]
        public IFormFile? ImageFile { get; set; }
        
        [FromForm(Name = "EbookFile")]
        public IFormFile? EbookFile { get; set; }
    }
}