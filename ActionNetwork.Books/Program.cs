using ActionNetwork.Books;
using System.Text.Json;

var client = new HttpClient();

await FetchAndProcessBooks();

async Task FetchAndProcessBooks()
{
    try
    {
        string url = "https://api.actionnetwork.com/web/v1/books";
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        BookResponse bookResponse = JsonSerializer.Deserialize<BookResponse>(responseBody, options);
        if (bookResponse != null)
        {
            ProcessBooks(bookResponse.Books);
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
    }
}

void ProcessBooks(List<Book> books)
{
    var filteredBooks = books
        .Where(book => book.ParentName != null && book.Meta.States.Any(state => state == "NJ" || state == "CO"))
        .ToList();

    var groupedBooks = filteredBooks
        .GroupBy(book => book.ParentName)
        .OrderBy(group => group.Key)
        .ToList();

    SaveBooksToFile(groupedBooks);
}

void SaveBooksToFile(IEnumerable<IGrouping<string, Book>> groupedBooks)
{
    var path = "result.txt";
    using (var writer = new StreamWriter(path))
    {
        foreach (var group in groupedBooks)
        {
            writer.WriteLine(group.Key);
            foreach (var book in group)
            {
                writer.WriteLine($"{book.DisplayName} {string.Join(", ", book.Meta.States)}");
            }
            writer.WriteLine();
        }
    }
    Console.WriteLine($"Data saved to {path}");
}