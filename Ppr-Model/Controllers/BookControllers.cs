using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ppr_Model.Entity;
using Ppr_Model.DBOperations;
using Ppr_Model.BookOperations.GetBooks;
using Ppr_Model.BookOperations.CreateBook;

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
        public Book GetById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
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
        public IActionResult UpdateBook(int id, [FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();
            book.GenreId = newBook.GenreId != default ? newBook.GenreId : book.GenreId;
            book.PageCount = newBook.PageCount != default ? newBook.PageCount : book.PageCount;
            book.PublishDate = newBook.PublishDate != default ? newBook.PublishDate : book.PublishDate;
            book.Title = !string.IsNullOrEmpty(newBook.Title) ? newBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}