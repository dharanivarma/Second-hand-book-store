using BookList.models;
using BookList.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BookController));
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _log4net.Info("Http GET is accessed");
                IEnumerable<Book> booklist = _bookRepository.GetAll();
                _log4net.Info("list is reviewed");
                return Ok(booklist);
            }
            catch
            {
                _log4net.Error("Error in Getting Book Details");
                return new NoContentResult();
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _log4net.Info("Http GETByid is accessed");
                var booklist = _bookRepository.GetById(id);
                _log4net.Info(booklist.Book_id + "th book is shown ");
                return new OkObjectResult(booklist);
            }
            catch
            {
                _log4net.Error("Error in Getting Book Details");
                return new NoContentResult();
            }
        }

       
        


    }
}
