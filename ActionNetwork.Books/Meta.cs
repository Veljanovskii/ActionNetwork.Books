using System.Text.Json.Serialization;

namespace ActionNetwork.Books;

public class Meta
{
    [JsonPropertyName("states")]
    public List<string> States { get; set; } = [];
}
