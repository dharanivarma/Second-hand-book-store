using AddBookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBookService.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext bookDbContext;

        public BookRepository(BookDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }
        public Book AddBook(Book book)
        {
            var result = bookDbContext.Books.Add(book);
            bookDbContext.SaveChanges();
            return book;
        }
    }
}
