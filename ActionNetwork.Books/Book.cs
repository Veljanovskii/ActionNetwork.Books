using System.Text.Json.Serialization;

namespace ActionNetwork.Books;

public class Book
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("meta")]
    public Meta Meta { get; set; } = new Meta();

    [JsonPropertyName("parent_name")]
    public string ParentName { get; set; } = string.Empty;

    [JsonPropertyName("book_parent_id")]
    public int? BookParentId { get; set; }
}
