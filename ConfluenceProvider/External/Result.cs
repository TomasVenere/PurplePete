using System.Text.Json.Serialization;

namespace PurplePete.ConfluenceProvider.External;

public record Result(
	[property: JsonPropertyName("content")] Content Content,
	[property: JsonPropertyName("title")] string Title,
	[property: JsonPropertyName("excerpt")] string Excerpt,
	[property: JsonPropertyName("url")] string Url,
	[property: JsonPropertyName("resultGlobalContainer")] ResultGlobalContainer ResultGlobalContainer,
	[property: JsonPropertyName("breadcrumbs")] IReadOnlyList<object> Breadcrumbs,
	[property: JsonPropertyName("entityType")] string EntityType,
	[property: JsonPropertyName("iconCssClass")] string IconCssClass,
	[property: JsonPropertyName("lastModified")] DateTime? LastModified,
	[property: JsonPropertyName("friendlyLastModified")] string FriendlyLastModified,
	[property: JsonPropertyName("score")] double? Score
);



