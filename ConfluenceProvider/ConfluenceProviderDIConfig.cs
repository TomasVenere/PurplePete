using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurplePete.ConfluenceProvider.Models;
using System.Net.Http.Headers;

namespace PurplePete.ConfluenceProvider;
public static class ConfluenceProviderDIConfig
{
	public static void AddConfluenceProvider(this IServiceCollection services, IConfiguration configuration)
	{
		Uri baseUri = configuration.GetRequiredSection("ServiceUrls").GetValue<Uri>("Confluence") ?? throw new Exception("Confluence URL not found in configuration");
		ConfluenceConfiguration secrets = configuration.GetRequiredSection("Secrets:Confluence").Get<ConfluenceConfiguration>() ?? throw new Exception("Confluence secrets not found in configuration");

		AuthenticationHeaderValue authHeader = new("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{secrets.User}:{secrets.Token}")));

		services.AddHttpClient<IConfluenceClient, ConfluenceClient>(client =>
		{
			client.BaseAddress = baseUri;
			client.DefaultRequestHeaders.Authorization = authHeader;
		});
	}
}
