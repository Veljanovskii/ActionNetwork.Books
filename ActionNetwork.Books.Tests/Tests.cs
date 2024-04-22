namespace ActionNetwork.Books.Tests;

[TestFixture]
public class Tests
{
    private List<Book> books;

    [SetUp]
    public void Setup()
    {
        books = new List<Book>
            {
                new Book { Id = 1, DisplayName = "Book A", ParentName = "Parent1", Meta = new Meta { States = new List<string> { "NJ", "NY" } } },
                new Book { Id = 2, DisplayName = "Book B", ParentName = "Parent2", Meta = new Meta { States = new List<string> { "CO", "CA" } } },
                new Book { Id = 3, DisplayName = "Book C", ParentName = null, Meta = new Meta { States = new List<string> { "NJ", "NV" } } },
                new Book { Id = 4, DisplayName = "Book D", ParentName = "Parent1", Meta = new Meta { States = new List<string> { "TX", "FL" } } }
            };
    }

    [Test]
    public void FilterBooks_ShouldExcludeBooksNotInNJorCOOrWithoutParentName()
    {
        var filteredBooks = books
            .Where(book => book.ParentName != null && book.Meta.States.Any(state => state == "NJ" || state == "CO"))
            .ToList();

        Assert.AreEqual(2, filteredBooks.Count);
        Assert.IsTrue(filteredBooks.All(book => book.ParentName != null));
        Assert.IsTrue(filteredBooks.Any(book => book.Meta.States.Contains("NJ")));
        Assert.IsTrue(filteredBooks.Any(book => book.Meta.States.Contains("CO")));
    }

    [Test]
    public void GroupBooks_ShouldGroupAndOrderBooksByParentName()
    {
        var filteredBooks = books
            .Where(book => book.ParentName != null && book.Meta.States.Any(state => state == "NJ" || state == "CO"))
            .ToList();

        var groupedBooks = filteredBooks
            .GroupBy(book => book.ParentName)
            .OrderBy(group => group.Key)
            .ToList();

        Assert.AreEqual(2, groupedBooks.Count);
        Assert.AreEqual("Parent1", groupedBooks[0].Key);
        Assert.AreEqual("Parent2", groupedBooks[1].Key);
    }
}