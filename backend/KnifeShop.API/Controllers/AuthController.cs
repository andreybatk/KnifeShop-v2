using KnifeShop.API.Contracts.Auth;
using KnifeShop.API.Contracts.Google;
using KnifeShop.API.Validators;
using KnifeShop.BL.Services;
using KnifeShop.DB.Models;
using KnifeShop.DB.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace KnifeShop.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly AuthenticatorService _authenticatorService;

        public AuthController(IConfiguration configuration, UserManager<User> userManager, RefreshTokenValidator refreshTokenValidator, IRefreshTokenRepository refreshTokenRepository, AuthenticatorService authenticatorService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
            _authenticatorService = authenticatorService;
        }

        [HttpPost("google-login")]
        [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GoogleAuth([FromBody] GoogleAuthRequest request)
        {
            var clientId = _configuration["Google:ClientId"];
            var validationUrl = $"https://oauth2.googleapis.com/tokeninfo?id_token={request.Token}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(validationUrl);
            var payload = JsonSerializer.Deserialize<GoogleTokenPayload>(response);

            if (payload?.Aud != clientId)
            {
                return Unauthorized(new { message = "Invalid token." });
            }

            return Ok(new { email = payload?.Email });
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registrationUser = new User { UserName = registerRequest.Username, Email = registerRequest.Email };

            IdentityResult result = await _userManager.CreateAsync(registrationUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors;
                return Conflict(new { errors });
            }

            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(loginRequest.Username);
            if (user == null)
            {
                return Unauthorized();
            }

            if(user.UserName == null)
            {
                return Unauthorized("Username is null.");
            }

            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isCorrectPassword)
            {
                return Unauthorized();
            }

            var authUser = await _authenticatorService.AuthenticateAsync(user);

            var response = new AuthenticatedUserResponse { AccessToken = authUser.AccessToken, RefreshToken = authUser.RefreshToken, AccessTokenExpirationTime = authUser.AccessTokenExpirationTime  };

            return Ok(response);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthenticatedUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                ModelState.AddModelError(nameof(refreshRequest.RefreshToken), "Invalid refresh token.");
                return BadRequest(ModelState);
            }

            var refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if (refreshTokenDTO == null)
            {
                ModelState.AddModelError(nameof(refreshRequest.RefreshToken), "Invalid refresh token.");
                return BadRequest(ModelState);
            }

            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);

            var user = await _userManager.FindByIdAsync(refreshTokenDTO.UserId.ToString());
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return BadRequest(ModelState);
            }

            var authUser = await _authenticatorService.AuthenticateAsync(user);

            var response = new AuthenticatedUserResponse
            {
                AccessToken = authUser.AccessToken,
                RefreshToken = authUser.RefreshToken,
                AccessTokenExpirationTime = authUser.AccessTokenExpirationTime
            };

            return Ok(response);
        }


        [HttpDelete("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            var rawUserId = HttpContext.User.FindFirstValue("ID");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _refreshTokenRepository.DeleteAll(userId);

            return NoContent();
        }
    }
}
