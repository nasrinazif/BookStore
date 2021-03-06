using AutoMapper;
using BookStore.API.Dtos.Book;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : MainController
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAll();

            var booksToReturn = _mapper.Map<IEnumerable<BookResultDto>>(books);

            return Ok(booksToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) return NotFound();

            var bookToReturn = _mapper.Map<BookResultDto>(book);

            return Ok(bookToReturn);
        }

        [HttpGet]
        [Route("get-books-by-category/{categoryId:int}")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _bookService.GetBooksByCategory(categoryId);

            if (!books.Any()) return NotFound();

            var booksToReturn = _mapper.Map<IEnumerable<BookResultDto>>(books);

            return Ok(booksToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookAddDto bookDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var book = _mapper.Map<Book>(bookDto);
            var bookResult = await _bookService.Add(book);

            if (bookResult == null) return BadRequest();

            var bookToReturn = _mapper.Map<BookResultDto>(bookResult);

            return Ok(bookToReturn);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, BookEditDto bookDto)
        {
            if (id != bookDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var book = _mapper.Map<Book>(bookDto);

            await _bookService.Update(book);

            return Ok(bookDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null) return NotFound();

            await _bookService.Remove(book);

            return Ok();
        }

        [Route("search/{bookName}")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Search(string bookName)
        {
            var booksSearchResult = await _bookService.Search(bookName);

            var books = _mapper.Map<List<Book>>(booksSearchResult);

            if (books == null || books.Count == 0) return NotFound("None book was founded");

            var booksToReturn = _mapper.Map<IEnumerable<BookResultDto>>(books);

            return Ok(booksToReturn);
        }

        [Route("search-book-with-category/{searchedValue}")]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchedValue)
        {
            var booksSearchResult = await _bookService.SearchBookWithCategory(searchedValue);

            var books = _mapper.Map<List<Book>>(booksSearchResult);

            if (!books.Any()) return NotFound("None book was founded");

            var booksToReturn = _mapper.Map<IEnumerable<BookResultDto>>(books);

            return Ok(booksToReturn);
        }
    }
}
