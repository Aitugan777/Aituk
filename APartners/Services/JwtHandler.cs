using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{

    public class JwtHandler : DelegatingHandler
    {
        private readonly TokenStore _tokenStore;
        private readonly IAuthService _authService;

        public JwtHandler(TokenStore tokenStore, IAuthService authService)
        {
            _tokenStore = tokenStore;
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_tokenStore.IsExpired)
            {
                var success = await _authService.RefreshTokenAsync();
                if (!success)
                {
                    // логика fallback, например — разлогинить пользователя
                }
            }

            if (!string.IsNullOrEmpty(_tokenStore.Token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.Token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
