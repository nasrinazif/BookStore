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
    public class BookController : MainController
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IMapper mapper, IBookService bookService)
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

            await _bookService.Update(_mapper.Map<Book>(bookDto));

            return Ok(bookDto);
        }
    }
}
