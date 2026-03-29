using BLL.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceImplementation
{
    public class AttachmentService : IAttachmentService
    {
        private static readonly List<string> AllowedExtensions = new List<string>
            {
                ".png",
                ".jpg",
                ".jpeg"
            };

        public const int MaxSize = 2_123_123;

        public string? Upload(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return null;

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrWhiteSpace(extension) || !AllowedExtensions.Contains(extension))
                return null;

            if (file.Length > MaxSize)
                return null;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            using var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);

            return $"files/Images/{fileName}";
        }

        public bool Delete(string filepath)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                return false;

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
                return true;
            }

            return false;
        }
    }
}
