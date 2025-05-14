using System.Text.Json.Serialization;

namespace PurplePete.ConfluenceProvider.External;

public record Expandable(
	[property: JsonPropertyName("container")] string Container,
	[property: JsonPropertyName("metadata")] string Metadata,
	[property: JsonPropertyName("extensions")] string Extensions,
	[property: JsonPropertyName("operations")] string Operations,
	[property: JsonPropertyName("children")] string Children,
	[property: JsonPropertyName("history")] string History,
	[property: JsonPropertyName("ancestors")] string Ancestors,
	[property: JsonPropertyName("body")] string Body,
	[property: JsonPropertyName("version")] string Version,
	[property: JsonPropertyName("descendants")] string Descendants,
	[property: JsonPropertyName("space")] string Space
);



