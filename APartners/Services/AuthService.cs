using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Authorize(string username, string password)
        {
            var loginModel = new LoginModel
            {
                Email = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (result != null && !string.IsNullOrEmpty(result.Token))
                {
                    PublicProperties.JWT = result.Token;
                    return true;
                }
            }

            return false;
        }

        public string GetToken()
        {
            return PublicProperties.JWT;
        }

        private class AuthResponse
        {
            public string Token { get; set; }
        }

        private class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }

}
