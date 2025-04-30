using KnifeShop.BL.Services.File;
using KnifeShop.Contracts.Category;
using KnifeShop.DB.Repositories.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnifeShop.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUploadFileService _fileService;

        public CategoryController(ICategoryRepository categoryRepository, IUploadFileService fileService)
        {
            _categoryRepository = categoryRepository;
            _fileService = fileService;
        }


        /// <remarks>
        /// Manager role required
        /// </remarks>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateCategoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Category name cannot be empty.");

            var imagePath = await _fileService.UploadImage(request.Image);

            await _categoryRepository.AddCategory(request.Name, imagePath);
            return Ok();
        }

        /// <remarks>
        /// Manager role required
        /// </remarks>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await _categoryRepository.RemoveCategory(id);
            if(!result)
            {
                return BadRequest("Failed to delete category or it has already been deleted");
            }

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryRepository.GetCategories();

            return Ok(result);
        }
    }
}
