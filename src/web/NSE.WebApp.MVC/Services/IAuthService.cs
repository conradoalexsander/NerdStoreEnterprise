using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<UserLoginResponse> Login(LoginUser loginUser);
        Task<UserLoginResponse> Register(RegisterUser loginUser);
    }
}
