using PurplePete.ConfluenceProvider.External;
using PurplePete.ConfluenceProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PurplePete.ConfluenceProvider;
public class ConfluenceClient : IConfluenceClient
{
	private readonly HttpClient _httpClient;
	private const string _searchUrl = "wiki/rest/api/search?limit=5&cql=";

	public ConfluenceClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<IEnumerable<Page>?> SearchForKeywords(IEnumerable<string> keywords)
	{
		if (keywords is null || !keywords.Any()) return null;

		StringBuilder urlBuilder = new("type=page");
		if(keywords.Skip(1).Any())
		{
			//More than 1 keyword
			urlBuilder.Append("AND (");
			urlBuilder.Append(string.Join(" AND ", keywords.Select(k => $"test~\"{k.Trim()}\"")));
			urlBuilder.Append(')');
		}
		else
		{
			//Only 1 keyword
			urlBuilder.Append($" AND text~\"{keywords.First()}\"");
		}
		string url = _searchUrl + Uri.EscapeDataString(urlBuilder.ToString());

		HttpResponseMessage response = await _httpClient.GetAsync(url);

		response.EnsureSuccessStatusCode();

		string json = await response.Content.ReadAsStringAsync();

		SearchResult result = await JsonSerializer.DeserializeAsync<SearchResult>( response.Content.ReadAsStream()) ?? throw new Exception("Could not deserialize search response");

		return result.Results.Select(r => new Page(r.Title, new Uri(_httpClient.BaseAddress!, r.Url), r.Excerpt));
	}
}
