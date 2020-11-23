using BookList.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int Book_id);
       
    }
}
