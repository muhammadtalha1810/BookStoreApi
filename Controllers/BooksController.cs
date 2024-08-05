using Microsoft.AspNetCore.Mvc;
using BookStoreApi.Models;
using BookStoreApi.BusinessLayer;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly BookBL bookBL;

        public BooksController(BookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            var books = bookBL.GetBooks();
            return Ok(books);
        }

        // GET: api/Books/search/The
        [HttpGet("search/{keyword}")]
        public ActionResult<IEnumerable<Book>> GetBooks(string keyword)
        {
            var books = bookBL.GetBooks(keyword);
            return Ok(books);
        }

        // GET: api/Books/3
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = bookBL.GetBook(id);

            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            return Ok(book);
        }

        // GET: api/Books/by/author
        [HttpGet("by/{author}")]
        public ActionResult<Book> GetBook(string author)
        {
            var book = bookBL.GetBook(author);

            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            bookBL.AddBook(book);
            //return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            return Ok(book);
        }

        // PUT: api/Books/2
        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book book)
        {
            var existingBook = bookBL.GetBook(id);
            if (existingBook == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            bookBL.UpdateBook(id, book);
            return Ok(new { Message = "Book updated successfully" });
        }

        // DELETE: api/Books/1
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var existingBook = bookBL.GetBook(id);
            if (existingBook == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            bookBL.DeleteBook(id);
            return Ok(new { Message = "Book deleted successfully" });
        }
    }
}
