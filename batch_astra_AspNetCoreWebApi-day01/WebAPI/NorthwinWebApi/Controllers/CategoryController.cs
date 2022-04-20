using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts;
using System;
using System.Linq;
using Northwind.Entities.DTO;
using AutoMapper;
using System.Collections.Generic;
using Northwind.Entities.Models;

namespace NorthwinWebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CategoryController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id,[FromBody] CategoryDto categoryDto)
        {
            if(categoryDto == null)
            {
                _logger.LogError($"(Category must not be null)");
                return BadRequest("Category must not be null");
            }

            var catgoryEntity = _repository.Category.GetCategory(id, trackChanges: true);
            if(catgoryEntity == null)
            {
                _logger.LogInfo($"Category with id : {id} not found");
                return NotFound();
            }

            _mapper.Map(categoryDto,catgoryEntity);
            _repository.Category.UpdateCategory(catgoryEntity);
            _repository.Save();
            return NoContent();


        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _repository.Category.GetCategory(id, trackChanges: false);
            if(category == null)
            {
                _logger.LogInfo($"Category with Id: {id} not found");
                return NotFound();
            }
            _repository.Category.DeleteCategory(category);
            _repository.Save();

            return NoContent();

        }

        [HttpPost] 
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                _logger.LogError("Category object is null");
                return BadRequest("Category object is null");
            }

            var categoryEntity = _mapper.Map<Category>(categoryDto);
            _repository.Category.CreateCategory(categoryEntity);
            _repository.Save();

            var categoryResult = _mapper.Map<CategoryDto>(categoryEntity);
            return CreatedAtRoute("CategoryById", new { id = categoryResult.CategoryId }, categoryResult);
        }

        [HttpGet("{id}",Name = "CategoryById")]
        public IActionResult GetCategory(int id)
        {
            var category = _repository.Category.GetCategory(id, trackChanges: true);
            if(category == null)
            {
                _logger.LogInfo($"Category with Id : {id} doesn't exist");
                return NotFound();
            }
            else
            {
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return Ok(categoryDto);
            }
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _repository.Category.GetAllCategory(trackChanges: false);
                //replace by categoryDTO
                //return Ok(categories);
               /* var categoryDto = categories.Select(c => new CategoryDto
                {
                    Id = c.CategoryId,
                    CategoryName = c.CategoryName,
                    description = c.Description
                }).ToList();*/
               var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetCategories)} message : {ex}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
