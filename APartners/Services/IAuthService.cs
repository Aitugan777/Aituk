using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="username">Почта</param>
        /// <param name="password">Пароль</param>
        /// <returns>токен</returns>
        Task<bool> Authorize(string username, string password);

        Task<bool> RefreshTokenAsync();
    }
}
