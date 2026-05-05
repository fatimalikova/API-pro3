using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace API_pro3.Extentions
{
    public static class FileManager
    {
        public static string SaveFile(this IFormFile file, string rootPath)
        {
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(rootPath, filename);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            return filename;
        }

        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool IsValidSize(this IFormFile file, int maxSizeInMB)
        {
            if (file == null) return true;
            return file.Length <= maxSizeInMB * 1024 * 1024;
        }
    }
}
