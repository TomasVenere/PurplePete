using System.Text.Json.Serialization;

namespace PurplePete.ConfluenceProvider.External;

public record SearchResult(
	[property: JsonPropertyName("results")] IReadOnlyList<Result> Results,
	[property: JsonPropertyName("start")] int? Start,
	[property: JsonPropertyName("limit")] int? Limit,
	[property: JsonPropertyName("size")] int? Size,
	[property: JsonPropertyName("totalSize")] int? TotalSize,
	[property: JsonPropertyName("cqlQuery")] string CqlQuery,
	[property: JsonPropertyName("searchDuration")] int? SearchDuration,
	[property: JsonPropertyName("_links")] Links Links
);



