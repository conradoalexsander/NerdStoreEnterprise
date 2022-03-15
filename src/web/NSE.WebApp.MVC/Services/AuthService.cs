using NSE.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            var loginContent = new StringContent(
                JsonSerializer.Serialize(loginUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44310/api/identity/authenticate", loginContent);


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            };

            if (!HandleErrosResponse(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserLoginResponse> Register(RegisterUser registerUser)
        {
            var registerContent = new StringContent(
                JsonSerializer.Serialize(registerUser),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44310/api/identity/new-account", registerContent);


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (!HandleErrosResponse(response))
            {
                return new UserLoginResponse
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}
