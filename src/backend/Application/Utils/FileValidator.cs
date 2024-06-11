using Microsoft.AspNetCore.Http;

namespace Application.Utils
{
    public static class FileValidator
    {
        public static bool IsFileExtensitonAllowed(IFormFile file, string[] allowedExtensions)
        {
            var extenstion = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(extenstion);
        }
        public static bool IsFileSizeWithinLimit(IFormFile file, long limit)
        {
            return file.Length<=limit;
        }
    }
}
