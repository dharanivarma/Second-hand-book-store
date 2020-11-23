using BookList;
using BookList.models;
using BookList.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BookListTest
{
    public class Tests
    {

        List<Book> book = new List<Book>();
        IQueryable<Book> bookdata;
        Mock<DbSet<Book>> mockSet;
        Mock<BookDbContext> bookcontextmock;
        [SetUp]
        public void Setup()
        {
            book = new List<Book>()
            {
                new Book{Book_id = 2, Book_Name="science",Cost=40 },
                  new Book{Book_id = 3, Book_Name="social",Cost=50 },
                    new Book{Book_id = 4, Book_Name="maths",Cost=100},
                      new Book{Book_id = 5, Book_Name="eng",Cost=70},

            };
            bookdata = book.AsQueryable();
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
        public void GetAllTest()
        {
            var bookrepo = new BookRepository(bookcontextmock.Object);
            var booklist = bookrepo.GetAll();
            Assert.AreEqual(4, booklist.Count());




        }
        [Test]
        public void GetByIdTest()
        {
            var bookrepo = new BookRepository(bookcontextmock.Object);
            var bookobj = bookrepo.GetById(2);
            Assert.IsNotNull(bookobj);
        }
        [Test]
        public void GetByIdTestFail()
        {
            var bookrepo = new BookRepository(bookcontextmock.Object);
            var bookobj = bookrepo.GetById(88);
            Assert.IsNull(bookobj);
        }
    }
}