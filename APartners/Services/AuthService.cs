using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public class AuthService : IAuthService
    {
        public async Task<bool> Authorize(string username, string password)
        {
            //Реализовать логику авторизации: отправляем запрос и получаем токен
            PublicProperties.JWT = "testToken";
            return true;
        }

        public string GetToken()
        {
            return PublicProperties.JWT;
        }
    }
}
