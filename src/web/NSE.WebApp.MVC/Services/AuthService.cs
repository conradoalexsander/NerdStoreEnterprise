using NSE.WebApp.MVC.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(LoginUser loginUser)
        {
            var loginContent = GetContent(loginUser);

            var response = await _httpClient.PostAsync("https://localhost:44310/api/identity/authenticate", loginContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(RegisterUser registerUser)
        {
            var registerContent = GetContent(registerUser);

            var response = await _httpClient.PostAsync("https://localhost:44310/api/identity/new-account", registerContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<UserLoginResponse>(response);
        }
    }
}
