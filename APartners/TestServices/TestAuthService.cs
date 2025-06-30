using APartners.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.TestServices
{
    public class TestAuthService : IAuthService
    {
        public Task<bool> Authorize(string username, string password)
        {
            return Task.FromResult(true);
        }

        public string GetToken()
        {
            return PublicProperties.JWT;
        }
    }
}
