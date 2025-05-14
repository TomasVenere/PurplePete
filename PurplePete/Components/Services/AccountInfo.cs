using System.Text.RegularExpressions;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;

namespace PurplePete.Services
{
    public class AccountInfo
    {
        private readonly IWebHostEnvironment _env;

        public AccountInfo(IWebHostEnvironment env)
        {
            _env = env;
        }

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
            return Regex.IsMatch(message, @"\d") && !Regex.IsMatch(message, @"^\d+$");
        }

        public string GetAccountStatus(string accountNumber)
        {
            return $"The account {accountNumber} is active.";
        }

        private async Task<List<Account>> LoadAccountsAsync()
        {
            var jsonPath = Path.Combine(_env.WebRootPath, "data", "accounts.json");
            if (!File.Exists(jsonPath))
                return new();

            using var stream = File.OpenRead(jsonPath);
            var accounts = await JsonSerializer.DeserializeAsync<List<Account>>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return accounts ?? new List<Account>();
        }

        public async Task<string> GetAllAccountsSummaryAsync()
        {
            var accounts = await LoadAccountsAsync();
            if (accounts.Count == 0)
                return "No accounts found.";

            var summary = new StringBuilder("This is a list of all the accounts:\n\n");
            foreach (var account in accounts)
            {
                summary.AppendLine($"- {account.AccountName} (#{account.AccountNumber}) — {account.AccountType}, {account.WorkflowState}");
            }

            summary.AppendLine("\nDo you want the full data for the accounts?");
            return summary.ToString();
        }

        public async Task<string> GetAllAccountsFullDataAsync()
        {
            var accounts = await LoadAccountsAsync();
            if (accounts.Count == 0)
                return "No account details available.";

            var sb = new StringBuilder("Here is the full data for all accounts:\n\n");

            foreach (var acc in accounts)
            {
                sb.AppendLine($"---\n**{acc.AccountName} (#{acc.AccountNumber})**");
                sb.AppendLine($"- Workflow: {acc.WorkflowState}");
                sb.AppendLine($"- Rebalance Setting: {acc.RebalanceSetting}");
                sb.AppendLine($"- Account Type: {acc.AccountType}");
                sb.AppendLine($"- Status: {acc.RebalanceStatus}");
                sb.AppendLine($"- Model: {acc.Model} ({acc.ModelDeviation})");
                sb.AppendLine($"- Buys/Sells: {acc.TotalBuys} / {acc.TotalSells}");
                sb.AppendLine($"- Cash: {acc.Cash} ({acc.CashPercentage})");
                sb.AppendLine($"- Total Cash: {acc.TotalCashPercentage}, Initial: {acc.InitialCashPercentage}");
                sb.AppendLine($"- Cash Reserve: {acc.CashReserveActual} / {acc.CashReserveGoal}");
                sb.AppendLine($"- Account Value: {acc.RebalancingAccountValue}");
                sb.AppendLine($"- YTD Gains: ST {acc.YtdStRealizedGain}, LT {acc.YtdLtRealizedGain}");
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    public class Account
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string WorkflowState { get; set; }
        public string RebalanceSetting { get; set; }
        public string AccountType { get; set; }
        public string RebalanceStatus { get; set; }
        public string Model { get; set; }
        public string ModelDeviation { get; set; }
        public string TotalBuys { get; set; }
        public string TotalSells { get; set; }
        public string Cash { get; set; }
        public string CashPercentage { get; set; }
        public string TotalCashPercentage { get; set; }
        public string InitialCashPercentage { get; set; }
        public string CashReserveActual { get; set; }
        public string CashReserveGoal { get; set; }
        public string RebalancingAccountValue { get; set; }
        public string YtdStRealizedGain { get; set; }
        public string YtdLtRealizedGain { get; set; }
        public string YtdCapitalGainsTaxes { get; set; }
        public string AnnualCapitalGainsTaxBudget { get; set; }
    }
}
