using System.Text.Json.Serialization;

namespace ActionNetwork.Books;

public class BookResponse
{
    [JsonPropertyName("books")]
    public List<Book> Books { get; set; } = [];
}
