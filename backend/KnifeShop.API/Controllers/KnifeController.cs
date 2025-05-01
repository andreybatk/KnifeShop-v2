using FluentValidation;
using FluentValidation.AspNetCore;
using KnifeShop.BL.Services.File;
using KnifeShop.Contracts.Knife;
using KnifeShop.DB.Repositories.Knifes;
using KnifeShop.DB.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        /// Manager role required
        /// </remarks>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateKnifeRequest request)
        {
            var validationResult = await _createKnifeValidator.ValidateAsync(request);

            if(validationResult.IsValid)
            {
                var imagePath = await _fileService.UploadImage(request.Image);
                var imagesPath = await _fileService.UploadImages(request.Images);

                var resultId = await _knifeRepository.Create(request.Title, request.CategoryIds, request.Description, imagePath, imagesPath, request.Price, request.IsOnSale,
                    request?.KnifeInfo?.OverallLength ?? null, request?.KnifeInfo?.BladeLength ?? null, request?.KnifeInfo?.ButtThickness ?? null, request?.KnifeInfo?.Weight ?? null, request?.KnifeInfo?.HandleMaterial ?? null, request?.KnifeInfo?.Country ?? null, request?.KnifeInfo?.Manufacturer ?? null, request?.KnifeInfo?.SteelGrade ?? null);

                return Ok(resultId);
            }

            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        /// <remarks>
        /// Manager role required
        /// </remarks>
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit([FromRoute] long id, [FromForm] EditKnifeRequest request)
        {
            var validationResult = await _editKnifeValidator.ValidateAsync(request);

            if (validationResult.IsValid)
            {
                var imagePath = await _fileService.UploadImage(request.Image);
                var imagesPath = await _fileService.UploadImages(request.Images);

                var resultId = await _knifeRepository.Edit(id, request.Title, request.CategoryIds, request.Description, imagePath, imagesPath, request.Price, request.IsOnSale,
                     request?.KnifeInfo?.OverallLength ?? null, request?.KnifeInfo?.BladeLength ?? null, request?.KnifeInfo?.ButtThickness ?? null, request?.KnifeInfo?.Weight ?? null, request?.KnifeInfo?.HandleMaterial ?? null, request?.KnifeInfo?.Country ?? null, request?.KnifeInfo?.Manufacturer ?? null, request?.KnifeInfo?.SteelGrade ?? null);

                if (resultId == 0)
                {
                    return BadRequest($"Edit knife with Id {id} is fault.");
                }

                return Ok(resultId);
            }

            validationResult.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(typeof(KnifesWithTotalCountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaginated([FromQuery] GetKnifesPaginationRequest request)
        {
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userIdString, out var userId);

            var (Items, TotalCount) = await _knifeRepository.GetPaginated(
                request.Search,
                request.SortItem,
                request.SortOrder,
                request.Page,
                request.PageSize,
                userId,
                request.CategoryId
            );

            var response = new KnifesWithTotalCountResponse
            {
                Knifes = Items,
                TotalCount = TotalCount
            };

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

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetKnifeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(userIdString, out var userId);

            var result = await _knifeRepository.Get(id, userId);

            if(result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <remarks>
        /// Manager role required
        /// </remarks>
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
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