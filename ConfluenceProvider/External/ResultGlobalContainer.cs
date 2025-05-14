using System.Text.Json.Serialization;

namespace PurplePete.ConfluenceProvider.External;

public record ResultGlobalContainer(
	[property: JsonPropertyName("title")] string Title,
	[property: JsonPropertyName("displayUrl")] string DisplayUrl
);



