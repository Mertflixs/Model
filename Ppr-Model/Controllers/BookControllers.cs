using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ppr_Model.Entity;
using Ppr_Model.DBOperations;
using Ppr_Model.BookOperations.GetBooks;
using Ppr_Model.BookOperations.CreateBook;
using Ppr_Model.BookOperations.DeleteBook;
using Ppr_Model.BookOperations.GetById;
using Ppr_Model.BookOperations.UpdateBook;

namespace Ppr_Model.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookControllers : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookControllers(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllBooks")]
        public IActionResult GetBook()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var res = query.Handle();
            return Ok(res);
        }

        [HttpGet("GetBookId/{id}")]
        public IActionResult GetById(int id)
        {
            GetById query = new GetById(_context, id);
            var res = query.Handle();
            return Ok(res);
        }

        //fromQuery ile 
        [HttpGet("GetBookQuery")]
        public Book GetByIdQuery([FromQuery] string id)
        {
            var book = _context.Books.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBook command = new CreateBook(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel newBook)
        {
            UpdateBook book = new UpdateBook(_context);
            try
            {
                book.Model = newBook;
                book.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBook book = new DeleteBook(_context, id);
            try
            {
                book.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}