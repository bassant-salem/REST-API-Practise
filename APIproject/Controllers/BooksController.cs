using APIproject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        static private List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "The Metamorphosis",
                Author = "Franz Kafka",
                YearPublished = 1960
            },
            new Book
            {
                Id = 2,
                Title = "1984",
                Author = "George Orwell",
                YearPublished = 1949
            },
            new Book
            {
                Id = 3,
                Title = "The Brothers Karamazov",
                Author = "Fyodor Dostoevsky",
                YearPublished = 1880
            },
            new Book
            {
                Id = 4,
                Title = "Being and Nothingness",
                Author = "Jean-Paul Sartre",
                YearPublished = 1943
            },
            new Book
            {
                Id = 5,
                Title = "The Trial",
                Author = "Franz Kafka",
                YearPublished = 1925
            }
        };

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            return Ok(books);
        }
        // The user requesting one specific resource   
        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public ActionResult<Book> AddBook(Book newBook)
        {
            if(newBook == null)
            {
                return BadRequest();
            }
            // Check if book with same title and author already exists
            var isDuplicate = books.Any(b =>
                b.Title == newBook.Title &&
                b.Author == newBook.Author);

            if (isDuplicate)
            {
                return BadRequest("A book with this title and author already exists.");
            }
            // Auto-generate ID if not provided
            newBook.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);

        }
        [HttpPut("{id}")]
        // We used here IActionResult because we are not returning any specific type or object
        // so the most appropriate return type is status code
        public IActionResult UpdateBook(int id,Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            books.Remove(book);
            return NoContent();
        }

    }
}
