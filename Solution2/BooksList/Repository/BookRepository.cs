using BookList.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext bookDbContext;

        public BookRepository(BookDbContext bookDbContext)
        {
            this.bookDbContext = bookDbContext;
        }

       

        public IEnumerable<Book> GetAll()
        {
            var booklist = bookDbContext.Books.ToList();
            return booklist;
            //  return  bookDbContext.Books.ToList();
        }
        public Book GetById(int Book_id)
        {
            return bookDbContext.Books.FirstOrDefault(b => b.Book_id == Book_id);
        }
    }
}
