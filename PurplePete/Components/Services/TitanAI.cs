using Amazon.Runtime;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PurplePete.Services
{
    public class TitanAI
    {
        private readonly HttpClient _httpClient;
        private readonly string _bedrockApiUrl = "https://bedrock-api-url.amazonaws.com";
        private readonly string _modelId = "titan-express"; 

        public TitanAI(AWSCredentials awsCredentials)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_ACCESS_TOKEN");
        }

        public async Task<string> GetResponseAsync(string inputMessage)
        {
            try
            {
                var requestBody = new
                {
                    prompt = inputMessage,
                    max_tokens = 1024,
                    temperature = 0.7
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_bedrockApiUrl}/invoke/{_modelId}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    return result?.text ?? "Error: Response body does not contain expected text.";
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
