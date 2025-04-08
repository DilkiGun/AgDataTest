using System.Text.RegularExpressions;

namespace UserManagement.Api.Utilities
{
    public static class UserValidator
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidPassword(string password)
        {
            return password.Length >= 8 && password.Any(char.IsDigit);
        }
    }
}
