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
        private readonly TokenStore _tokenStore;

        public AuthService(HttpClient httpClient, TokenStore tokenStore)
        {
            _httpClient = httpClient;
            _tokenStore = tokenStore;
        }

        public async Task<bool> Authorize(string username, string password)
        {
            var loginModel = new { Email = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginModel);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                _tokenStore.SetToken(result.Token, result.ExpiresIn);
                return true;
            }

            return false;
        }

        public async Task<bool> RefreshTokenAsync()
        {
            var response = await _httpClient.GetAsync("api/Auth/refresh");
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (result != null && !string.IsNullOrEmpty(result.Token))
            {
                _tokenStore.SetToken(result.Token, result.ExpiresIn);
                return true;
            }

            return false;
        }

        private class AuthResponse
        {
            public string Token { get; set; }
            public int ExpiresIn { get; set; } // seconds
        }
    }


}
