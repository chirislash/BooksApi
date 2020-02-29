using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        public BooksController(BookService booksService) {
            _bookService = booksService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(String id) {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id.ToString(), book });
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(String id, Book bookIn)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Update(id,bookIn);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(String id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book);
            return NoContent();
        }

    }
}
