using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PurplePete.Services
{
    public class BedrockChatAI
    {
        private const string DefaultResponse = "Hi! I'm Purple Pete. We're currently under maintenance, but feel free to leave your question!";

        public Task<string> GetResponseAsync(string? rawResponse, string? userInput = null)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
            {
                Console.WriteLine("[BedrockChatAI] Empty or null response. Returning default.");
                return Task.FromResult(DefaultResponse);
            }

            // Check user input for no-AI flag
            bool useRaw = !string.IsNullOrWhiteSpace(userInput) &&
                          Regex.IsMatch(userInput, @"\bno[\s\-]?ai\b", RegexOptions.IgnoreCase);

            if (useRaw)
            {
                Console.WriteLine("[BedrockChatAI] 'noai' detected in user input — skipping formatting.");
                return Task.FromResult("No AI:\n" + rawResponse);
            }

            // Otherwise, apply light formatting
            var cleaned = rawResponse
                .Trim()
                .Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Replace("\n\n", "\n\n");

            Console.WriteLine("[BedrockChatAI] Cleaned response prepared.");
            return Task.FromResult("With AI:\n" + cleaned);
        }
    }
}
