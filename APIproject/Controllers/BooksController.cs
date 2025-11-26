using APIproject.Data;
using APIproject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //static private List<Book> books = new List<Book>
        //{
        //    new Book
        //    {
        //        Id = 1,
        //        Title = "The Metamorphosis",
        //        Author = "Franz Kafka",
        //        YearPublished = 1960
        //    },
        //    new Book
        //    {
        //        Id = 2,
        //        Title = "1984",
        //        Author = "George Orwell",
        //        YearPublished = 1949
        //    },
        //    new Book
        //    {
        //        Id = 3,
        //        Title = "The Brothers Karamazov",
        //        Author = "Fyodor Dostoevsky",
        //        YearPublished = 1880
        //    },
        //    new Book
        //    {
        //        Id = 4,
        //        Title = "Being and Nothingness",
        //        Author = "Jean-Paul Sartre",
        //        YearPublished = 1943
        //    },
        //    new Book
        //    {
        //        Id = 5,
        //        Title = "The Trial",
        //        Author = "Franz Kafka",
        //        YearPublished = 1925
        //    }
        //};
        private readonly APIprojectContext _context;
        public BooksController(APIprojectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            return Ok(await _context.Books.ToListAsync());
        }
        // The user requesting one specific resource   
        [HttpGet("{id}")]
        public async  Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task <ActionResult<Book>> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }
            
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);

        }
        [HttpPut("{id}")]
        // We used here IActionResult because we are not returning any specific type or object
        // so the most appropriate return type is status code
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
