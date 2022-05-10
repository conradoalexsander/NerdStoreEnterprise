using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identity.API.Extensions;
using NSE.Identity.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Identity.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppSettings> appSettings, 
            ITokenHandler tokenHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                return CustomResponse(await _tokenHandler.GenerateJWT(registerUser.Email));
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

            var result = await _signInManager.PasswordSignInAsync(
                    loginUser.Email,
                    password: loginUser.Password,
                    isPersistent: false,
                    lockoutOnFailure: true);
            //lockoutOnFailure: lock the loging in the application for an amount of time if there were too many failed logins attempts. 

            if (result.Succeeded)
            {
                return CustomResponse(await _tokenHandler.GenerateJWT(loginUser.Email));
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
