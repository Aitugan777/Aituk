using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class TokenStore
    {
        public string Token { get; private set; } = string.Empty;
        private DateTime _expiresAt = DateTime.MinValue;

        public bool IsExpired => DateTime.UtcNow >= _expiresAt;

        public void SetToken(string token, int expiresInSeconds)
        {
            Token = token;
            _expiresAt = DateTime.UtcNow.AddSeconds(expiresInSeconds - 30); // с запасом
        }

        public void Clear()
        {
            Token = string.Empty;
            _expiresAt = DateTime.MinValue;
        }
    }
}
