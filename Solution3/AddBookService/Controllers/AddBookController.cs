using AddBookService.Models;
using AddBookService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddBookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AddBookController));
        public AddBookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                _log4net.Info("Book Details Getting Added");
                if (ModelState.IsValid)
                {

                    var buyobj = _bookRepository.AddBook(book);
                    // return Created("", book);
                    _log4net.Info("added " + buyobj.Book_Name + " details");

                    return CreatedAtAction(nameof(Post), new { id = book.Book_id }, book);

                }
                return BadRequest();


            }
            catch
            {
                _log4net.Info("Book Details Getting Added");
                return new NoContentResult();
            }

        }
    }
}
