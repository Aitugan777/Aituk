using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APartners.Services
{
    /// <summary>
    /// Публичные статические переменные
    /// </summary>
    public static class PublicProperties
    {
        public const string ServerURL = "url сервера";

        public static string JWT { get; set; }
    }
}
