using System.Text.RegularExpressions;

namespace Application.Utils
{
    public static class ValidationExtension
    {
        public static async Task<bool> ValidateEmail(string email, CancellationToken cancellationToken = default)
        {
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public static bool ValidatePhone(string phone)
        {
            string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phone);
        }
        public static async Task<bool> IsValidUsername(string username, CancellationToken cancellationToken = default)
        {
            string pattern = @"^[a-zA-Z0-9]{5,15}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }
        public static async Task<bool> ValidateSlug(string slug, CancellationToken cancellationToken = default)
        {
            string pattern = @"^[a-z0-9]+(?:-[a-z0-9]+)*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(slug);
        }
    }
}
