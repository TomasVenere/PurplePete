using System.Text.Json.Serialization;

namespace PurplePete.ConfluenceProvider.External;

public record Links(
	[property: JsonPropertyName("webui")] string Webui,
	[property: JsonPropertyName("self")] string Self,
	[property: JsonPropertyName("tinyui")] string Tinyui,
	[property: JsonPropertyName("base")] string Base,
	[property: JsonPropertyName("context")] string Context,
	[property: JsonPropertyName("next")] string Next
);



