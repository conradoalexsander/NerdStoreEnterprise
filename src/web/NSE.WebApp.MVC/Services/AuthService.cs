using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationAPI.BaseUrl);

            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<UserLoginResponse> Login(LoginUser loginUser)
        {
            var loginContent = GetContent(loginUser);

            var response = await _httpClient.PostAsync($"{_settings.AuthenticationAPI.AuthenticatePath}", loginContent);

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

            var response = await _httpClient.PostAsync($"{_settings.AuthenticationAPI.NewAccountPath}", registerContent);

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
