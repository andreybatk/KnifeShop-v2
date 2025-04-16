using FluentValidation;
using FluentValidation.AspNetCore;
using KnifeShop.API.Contracts.Knife;
using KnifeShop.BL.Services;
using KnifeShop.DB.Models;
using KnifeShop.DB.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnifeShop.API.Controllers
{
    [Route("api/knife")]
    [ApiController]
    public class KnifeController : ControllerBase
    {
        private readonly IKnifeRepository _knifeRepository;
        private readonly IUploadFileService _fileService;

        private readonly IValidator<CreateKnifeRequest> _createKnifeValidator;
        private readonly IValidator<EditKnifeRequest> _editKnifeValidator;

        public KnifeController(IKnifeRepository knifeRepository, IUploadFileService fileService, IValidator<CreateKnifeRequest> createKnifeValidator, IValidator<EditKnifeRequest> editKnifeValidator)
        {
            _knifeRepository = knifeRepository;
            _fileService = fileService;
            _createKnifeValidator = createKnifeValidator;
            _editKnifeValidator = editKnifeValidator;
        }

        /// <remarks>
        /// Admin role required
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateKnifeRequest request)
        {
            var validationResult = await _createKnifeValidator.ValidateAsync(request);

            if(validationResult.IsValid)
            {
                var imagePath = await _fileService.UploadImage(request.Image);
                var imagesPath = await _fileService.UploadImages(request.Images);

                var id = await _knifeRepository.Create(request.Title, request.Category, request.Description, imagePath, imagesPath, request.Price, request.IsOnSale,
                    request?.KnifeInfo?.OverallLength ?? null, request?.KnifeInfo?.BladeLength ?? null, request?.KnifeInfo?.ButtThickness ?? null, request?.KnifeInfo?.Weight ?? null, request?.KnifeInfo?.HandleMaterial ?? null, request?.KnifeInfo?.Country ?? null, request?.KnifeInfo?.Manufacturer ?? null, request?.KnifeInfo?.SteelGrade ?? null);

                return Ok(id);
            }

            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        /// <remarks>
        /// Admin role required
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromRoute] long id, [FromForm] EditKnifeRequest request)
        {
            var validationResult = await _editKnifeValidator.ValidateAsync(request);

            if (validationResult.IsValid)
            {
                var imagePath = await _fileService.UploadImage(request.Image);
                var imagesPath = await _fileService.UploadImages(request.Images);

                var result = await _knifeRepository.Edit(id, request.Title, request.Category, request.Description, imagePath, imagesPath, request.Price, request.IsOnSale,
                     request?.KnifeInfo?.OverallLength ?? null, request?.KnifeInfo?.BladeLength ?? null, request?.KnifeInfo?.ButtThickness ?? null, request?.KnifeInfo?.Weight ?? null, request?.KnifeInfo?.HandleMaterial ?? null, request?.KnifeInfo?.Country ?? null, request?.KnifeInfo?.Manufacturer ?? null, request?.KnifeInfo?.SteelGrade ?? null);

                if (result is null)
                {
                    return BadRequest($"Edit knife with Id {id} is fault.");
                }

                return Ok();
            }

            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(List<GetKnifesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaginated([FromQuery] GetKnifesPaginationRequest request)
        {
            var result = await _knifeRepository.GetPaginated(
                request.Search,
                request.SortItem,
                request.SortOrder,
                request.Page,
                request.PageSize
            );

            var response = new List<GetKnifesResponse>(result.TotalCount);
            
            foreach( var item in result.Items )
            {
                response.Add(new GetKnifesResponse { Id = item.Id, Title = item.Title, Category = item.Category, Image = item.Image, Price = item.Price, IsOnSale = item.IsOnSale });
            }

            return Ok(response);
        }

        [HttpGet("on_sale")]
        [ProducesResponseType(typeof(List<GetKnifesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOnSale([FromQuery] GetKnifesRequest request)
        {
            var result = await _knifeRepository.GetOnSale(
                request.Search,
                request.SortItem,
                request.SortOrder
            );

            var response = new List<GetKnifesResponse>(result.Count);

            foreach (var item in result)
            {
                response.Add(new GetKnifesResponse { Id = item.Id, Title = item.Title, Category = item.Category, Image = item.Image, Price = item.Price, IsOnSale = item.IsOnSale });
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Knife), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var result = await _knifeRepository.Get(id);

            if(result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <remarks>
        /// Admin role required
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var result = await _knifeRepository.Delete(id);

            if (result != 0)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}