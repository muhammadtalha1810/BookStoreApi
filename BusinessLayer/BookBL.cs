using BookStoreApi.Models;
using BookStoreApi.DataAccessLayer;
namespace BookStoreApi.BusinessLayer
{
    public class BookBL
    {

        private readonly BookDAL bookDAL;

        public BookBL(BookDAL bookDAL)
        {
            this.bookDAL = bookDAL;
        }
        public List<Book> GetBooks()
        {
            return bookDAL.GetBooks();
        }

        public List<Book> GetBooks(string keyword)
        {
            return bookDAL.GetBooks(keyword);
        }

        public Book GetBook(int id)
        {
            return bookDAL.GetBook(id);
        }

        public List<Book> GetBook(string author)
        {
            return bookDAL.GetBook(author);
        }

        public void AddBook(Book book)
        {
            bookDAL.AddBook(book);
        }

        public void UpdateBook(int id, Book book)
        {
            var existingBook = bookDAL.GetBook(id);
            if (existingBook != null)
            {
                book.Id = id; // Ensure the ID remains unchanged
                bookDAL.UpdateBook(book);
            }
        }

        public void DeleteBook(int id)
        {
            bookDAL.DeleteBook(id);
        }
    }
}
