using AutoMapper;
using Library.WebApi.DataTransferObject;
using Library.WebApi.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a collection of categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCollectionOfCategories()
        {
            var categoryCollection = await _categoryService.GetCollectionOfCategories();

            if (!categoryCollection.Any())
            {
                return NoContent();
            }

            var mappedResult = _mapper.Map<List<CategoryResponseDto>>(categoryCollection);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Add a catagory.
        /// </summary>
        /// <param name="categoryRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDto categoryRequestDto)
        {
            var categoryAdded = await _categoryService.CreateCategory(categoryRequestDto);

            if (!categoryAdded)
            {
                return UnprocessableEntity();
            }

            return Ok(categoryAdded);
        }

        /// <summary>
        /// Edit a category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="categoryRequestDto"></param>
        /// <returns></returns>
        [HttpPut("{categoryId}")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> EditCategory([FromRoute] int categoryId, [FromBody] CategoryRequestDto categoryRequestDto)
        {
            var editCategory = await _categoryService.EditCategory(categoryId, categoryRequestDto);

            if (editCategory == null)
            {
                return UnprocessableEntity();
            }

            var mappedResult = _mapper.Map<CategoryResponseDto>(editCategory);

            return Ok(mappedResult);
        }

        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            var deleteCategory = await _categoryService.DeleteCategory(categoryId);

            if (!deleteCategory)
            {
                return UnprocessableEntity();
            }

            return Ok(deleteCategory);
        }

    }
}
