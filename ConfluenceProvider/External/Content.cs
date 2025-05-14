using System.Text.Json.Serialization;

namespace PurplePete.ConfluenceProvider.External;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

public record Content(
	[property: JsonPropertyName("id")] string Id,
	[property: JsonPropertyName("type")] string Type,
	[property: JsonPropertyName("status")] string Status,
	[property: JsonPropertyName("title")] string Title,
	[property: JsonPropertyName("_expandable")] Expandable Expandable,
	[property: JsonPropertyName("_links")] Links Links
);



