using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NSE.Identity.API.Models;

namespace NSE.Identity.API.Services {
    
    public interface IAuthService 
    {
        Task<IdentityResult> CreateUserAsync(RegisterUser registerUser);
        Task<SignInResult> SignInUserAsync(LoginUser loginUser);
    }
    
    public class AuthService : IAuthService 
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        
        public AuthService(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterUser registerUser) 
        {
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

           return await _userManager.CreateAsync(user, registerUser.Password);
        }       
        
        public async Task<SignInResult> SignInUserAsync(LoginUser loginUser) 
        {
            return  await _signInManager.PasswordSignInAsync(
               loginUser.Email,
               password: loginUser.Password,
               isPersistent: false,
               lockoutOnFailure: true); //lockoutOnFailure: lock the loging in the application for an amount of time if there were too many failed logins attempts. 
        }
    }
}