using BookStoreApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json.Nodes;
namespace BookStoreApi.DataAccessLayer
{
    public class BookDAL
    {
        private readonly string _connectionString;
        public BookDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "A Year Of Last Things", Author = "Michael Ondaatje", Price = "Rs 3,495.00", ImageUrl = "./images/img4.jpg" },
            new Book { Id = 2, Title = "A Fate Inked In Blood", Author = "Danielle L. Jensen", Price = "Rs 3,495.00", ImageUrl = "./images/img5.jpg" },
            new Book { Id = 3, Title = "The Big Fail", Author = "Bethany McLean", Price = "Rs 4,295.00", ImageUrl = "./images/img6.jpg" },
            new Book { Id = 4, Title = "Stand Up Straight", Author = "Paul Nanson", Price = "Rs 3,095.00", ImageUrl = "./images/img7.jpg" },
            new Book { Id = 5, Title = "The Everything Token", Author = "Steve Kaczynski", Price = "Rs 3,995.00", ImageUrl = "./images/img8.jpg" },
            new Book { Id = 6, Title = "Impossible Monsters", Author = "Michael Taylor", Price = "Rs 3,995.00", ImageUrl = "./images/img9.jpg" },
            new Book { Id = 7, Title = "The Girl Who Wasn't There", Author = "Jacqueline Wilson", Price = "Rs 3,095.00", ImageUrl = "./images/img10.jpg" },
            new Book { Id = 8, Title = "House Rules Wild Cards", Author = "George R.R. Martin", Price = "Rs 4,595.00", ImageUrl = "./images/img11.jpg" },
            new Book { Id = 9, Title = "Night Watch", Author = "Jayne Anne Phillips", Price = "Rs 3,795.00", ImageUrl = "./images/img12.jpg" },
            new Book { Id = 10, Title = "Navola", Author = "Paolo Bacigalupi", Price = "Rs 4,295.00", ImageUrl = "./images/img13.jpg" },
        };

        public List<Book> GetBooks()
        {
            return books;
        }

        public List<Book> GetProducts()
        {
            var books = new List<Book>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllBooks", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new Book
                        {
                            Id = (int)reader["Id"],
                            Title = reader["Title"].ToString(),
                            Author = reader["Author"].ToString(),
                            Price = (decimal)reader["Price"],
                            ImageUrl = reader["ImageUrl"].ToString()
                        };

                        books.Add(book);
                    }
                }
            }

            return books;
        }

        public Book GetBook(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetBook(string Author)
        {
            //Currently it only returns one book, in future modify it.
            return books.FindAll(b => b.Author == Author);
            //return books.FirstOrDefault(b => b.Author == Author);
        }

        public void AddBook(Book book)
        {
            book.Id = books.Max(b => b.Id) + 1;
            books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var existingBook = books.FirstOrDefault(b => b.Id == book.Id);

            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.ImageUrl = book.ImageUrl;
            }
        }

        public void DeleteBook(int id)
        {
            var book = books.First(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
            }
        }
    }
}
