using AddBookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBookService.Repository
{
    public interface IBookRepository
    {
        Book AddBook(Book book);
    }
}
