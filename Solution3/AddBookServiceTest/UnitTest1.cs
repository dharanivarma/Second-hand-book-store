using AddBookService;
using AddBookService.Models;
using AddBookService.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AddBookServiceTest
{
    public class Tests
    {

        List<Book> books = new List<Book>();
        IQueryable<Book> bookdata;
        Mock<DbSet<Book>> mockSet;
        Mock<BookDbContext> bookcontextmock;
        [SetUp]
        public void Setup()
        {
            books = new List<Book>()
            {
                 new Book{Book_id = 2,Book_Name="science",Cost=40},
                 new Book{Book_id = 3,Book_Name="social",Cost=50},

            };
            bookdata = books.AsQueryable();
            mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(bookdata.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(bookdata.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(bookdata.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(bookdata.GetEnumerator());
            var p = new DbContextOptions<BookDbContext>();
            bookcontextmock = new Mock<BookDbContext>(p);
            bookcontextmock.Setup(x => x.Books).Returns(mockSet.Object);



        }

        [Test]
        public void AddBookingDetailTest()
        {
            var bookrepo = new BookRepository(bookcontextmock.Object);
            var bookobj = bookrepo.AddBook(new Book { Book_id = 3, Book_Name= "social",Cost = 50 });
            Assert.IsNotNull(bookobj);
        }

    }

}
