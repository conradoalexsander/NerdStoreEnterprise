using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSE.Identity.API.Extensions;
using NSE.Identity.API.Models;
using System.Threading.Tasks;
using NSE.Identity.API.Services;

namespace NSE.Identity.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(
            IOptions<AppSettings> appSettings, 
            ITokenService tokenService, 
            IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            var result = await _authService.CreateUserAsync(registerUser);

            if (result.Succeeded)
            {
                return CustomResponse(await _tokenService.GenerateJWT(registerUser.Email));
            }

            foreach (var error in result.Errors)
            {
                AddProcessingError(error.Description);
            }

            return CustomResponse();

        }

        [HttpPost("authenticate")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse();

            var result = await _authService.SignInUserAsync(loginUser);

            if (result.Succeeded)
            {
                return CustomResponse(await _tokenService.GenerateJWT(loginUser.Email));
            }

            if (result.IsLockedOut)
            {
                AddProcessingError("Too many invalid attempts to login. The user is temporaly blocked");
                return CustomResponse();
            }

            AddProcessingError("Incorrect password or user");

            return CustomResponse();
        }

    }
}
