using KnifeShop.API.Contracts.Knife;
using KnifeShop.API.Contracts.User;
using KnifeShop.DB.Contracts;
using KnifeShop.DB.Enums;
using KnifeShop.DB.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KnifeShop.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("favorite_knife/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddFavoriteKnife([FromRoute] long id)
        {
            var userIdString = this.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (!Guid.TryParse(userIdString, out var userId))
                return BadRequest("Invalid user ID.");

            var result = await _userRepository.AddFavoriteKnife(id, userId);

            return result switch
            {
                AddFavoriteResult.Success => Ok(),
                AddFavoriteResult.AlreadyExists => BadRequest("Knife is already exists in favorites."),
                AddFavoriteResult.KnifeNotFound => NotFound("Knife not found."),
                _ => StatusCode(500)
            };
        }

        [HttpDelete("favorite_knife/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFavoriteKnife([FromRoute] long id)
        {
            var userIdString = this.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (!Guid.TryParse(userIdString, out var userId))
                return BadRequest("Invalid user ID.");

            var result = await _userRepository.RemoveFavoriteKnife(id, userId);

            return result switch
            {
                AddFavoriteResult.Success => Ok(),
                AddFavoriteResult.KnifeNotFound => NotFound("Knife not found."),
                _ => StatusCode(500)
            };
        }

        [HttpGet("favorite_knifes")]
        [ProducesResponseType(typeof(List<GetKnifesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFavoriteKnives([FromQuery] GetFavoriteKnifesPaginated request)
        {
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdString, out var userId))
                return BadRequest("Invalid user ID.");

            var result = await _userRepository.GetFavoriteKnives(userId, request.Page, request.PageSize);

            var response = new KnifesWithTotalCountResponse
            {
                Knifes = result.Items,
                TotalCount = result.TotalCount
            };

            return Ok(response);
        }
    }
}
