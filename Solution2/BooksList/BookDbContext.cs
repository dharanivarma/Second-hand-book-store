using BookList.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList
{
    public class BookDbContext:DbContext  
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Book> Books { get; set; }
    }
}
