using System.Text.RegularExpressions;

namespace Application.Utils
{
    public static class ValidationExtension
    {
        public static bool ValidationEmail(string email)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(email);
        }
        public static bool ValidationPhone(string phone)
        {
            Regex regex = new Regex(@"^0[0-9]{9}$");
            return regex.IsMatch(phone);
        }
        public static bool ValidationConfirmPassword(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }
        public static bool validationSlug(string slug)
        {
            Regex regex = new Regex(@"^[a-z0-9-]{1,100}$");
            return regex.IsMatch(slug);
        }
        public static bool ValidationAddress(string address)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9 ]{1,100}$");
            return regex.IsMatch(address);
        }
        public static bool ValidationName(string name)
        {
            Regex regex = new Regex(@"^[a-zA-Z ]{1,50}$");
            return regex.IsMatch(name);
        }
        public static bool ValidationUsername(string username)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9]{6,15}$");
            return regex.IsMatch(username);
        }
        public static bool ValidationPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            return regex.IsMatch(password);
        }
    }
}
