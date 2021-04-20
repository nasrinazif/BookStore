using AutoMapper;
using BookStore.API.Dtos.Category;
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
    public class CategoriesController : MainController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest();

            var categories = await _categoryService.GetAll();

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryResultDto>>(categories);

            return Ok(categoriesToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = await _categoryService.GetById(id);

            if (category == null) return NotFound();

            var categoryToReturn = _mapper.Map<CategoryResultDto>(category);

            return Ok(categoryToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryToAdd)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = _mapper.Map<Category>(categoryToAdd);
            var categoryResult = await _categoryService.Add(category);

            if (categoryResult == null) return BadRequest();

            var categoryToReturn = _mapper.Map<CategoryResultDto>(categoryResult);

            return Ok(categoryToReturn);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CategoryEditDto categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var category = _mapper.Map<Category>(categoryDto);

            await _categoryService.Update(category);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();

            var result = await _categoryService.Remove(category);

            if (!result) return BadRequest();

            return Ok();
        }

        [Route("search/{category}")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Search(string category)
        {
            var categoriesSearchResult = await _categoryService.Search(category);
            var categories = _mapper.Map<List<Category>>(categoriesSearchResult);

            if (categories == null || categories.Count == 0)
                return NotFound("No categories were founded");

            return Ok(categories);
        }
    }
}
