using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.DTOs
{
    public class UploadAvatarFilesDto
    {
        [FromForm(Name = "Avatar")]
        public IFormFile? Avatar { get; set; }
    }
}