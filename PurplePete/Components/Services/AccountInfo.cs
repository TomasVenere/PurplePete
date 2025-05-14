using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PurplePete.Services
{
    public class AccountInfo
    {
        public bool ContainsAccountKeyword(string message)
        {
            return Regex.IsMatch(message, @"\baccount\b", RegexOptions.IgnoreCase);
        }

        public bool IsValidAccountNumber(string message)
        {
            return Regex.IsMatch(message, @"^\d{6,}$"); // 6+ digit number
        }

        public bool HasInvalidCharacters(string message)
        {
            // Contains some digits, but also invalid characters
            return Regex.IsMatch(message, @"\d") && !Regex.IsMatch(message, @"^\d+$");
        }

        public string GetAccountStatus(string accountNumber)
        {
            // This can later be expanded to call a real service
            return $"The account {accountNumber} is active.";
        }
    }
}
